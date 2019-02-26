using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        DbPostrgreContext CommonDbContext;

        public RoleRepository(DbPostrgreContext commonDbContext) : base(commonDbContext)
        {
            CommonDbContext = commonDbContext; ;
        }

        public string FindNameById(int id)
        {
            return CommonDbContext.Roles.FirstOrDefault(
                _ => _.Id == id).Name;
        }

        public int FindIdByName(string name)
        {
            return CommonDbContext.Roles.FirstOrDefault(_ => _.Name == name).Id;
        }
    }
}
