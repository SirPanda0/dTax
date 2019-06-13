using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
 
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using dTax.Auth;
using dTax.Data.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using dTax.Common;
using dTax.Data.Repository;
using dTax.Entity;

namespace dTax
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DbPostrgreContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Первая версия API ", //dTax-mailing@yandex.ru M9S206
                });
            });


        #region RepositoryRegion
        services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IFileStorageRepository, FileStorageRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<ICabRepository, CabRepository>();

            services.AddTransient<ICabRideRepository, CabRideRepository>();
            services.AddTransient<ICabRideStatusRepository, CabRideStatusRepository>();
            services.AddTransient<IPaymentTypeRepository, PaymentTypeRepository>();

            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IStatusesRepository, StatusesRepository>();

            services.AddTransient<IDriverFileRepository, DriverFileRepository>();

            services.AddTransient<ICabFileRepository, CabFileRepository>();

            services.AddTransient<ICarBrandRepository, CarBrandRepository>();
            services.AddTransient<ICarColorRepository, CarColorRepository>();
            services.AddTransient<ICarModelRepository, CarModelRepository>();
            services.AddTransient<ICarTypeRepository, CarTypeRepository>();
            services.AddTransient<IFileContentRepository, FileContentRepository>();

            services.AddTransient<IDBWorkFlow, DBWorkFlow>();
            #endregion

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {

               options.Cookie.Name = "dTaxCookie";
               options.ExpireTimeSpan =TimeSpan.FromDays(2);
               options.SlidingExpiration = false;
               options.Events = new CookieAuthenticationEvents
               {
                   OnRedirectToLogin = context =>
                   {
                       context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                       return Task.CompletedTask;
                   },
                   OnRedirectToAccessDenied = context =>
                   {
                       context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                       return Task.CompletedTask;
                   }
               };

           });

            services.AddSingleton<IAuthorizationHandler, AuthRoleHandler>();

            services.Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy(AuthorizePolicyValues.Operator,
                    policyBuilder => policyBuilder.AddRequirements(new RoleRequirement(AuthenticationRole.Operator)));
                options.AddPolicy(AuthorizePolicyValues.User,
                    policyBuilder => policyBuilder.AddRequirements(new RoleRequirement(AuthenticationRole.User)));
                options.AddPolicy(AuthorizePolicyValues.Driver,
                    policyBuilder => policyBuilder.AddRequirements(new RoleRequirement(AuthenticationRole.Driver)));
                options.AddPolicy(AuthorizePolicyValues.FullAccess,
                    policyBuilder => policyBuilder.RequireClaim(CustomClaimType.FullAccess, "True"));
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                    .AllowAnyHeader()
                        .AllowCredentials()
                        );

            //app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

            app.UseAuthentication();

            app.UseSwagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}");

            });
        }
    }
}
