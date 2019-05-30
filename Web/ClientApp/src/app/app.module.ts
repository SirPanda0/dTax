import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { RegistrComponent } from './registr/registr.component';
import { VoditregistrComponent } from './voditregistr/voditregistr.component';
import { AvtoregistrComponent } from './avtoregistr/avtoregistr.component';
import { DriverComponent } from './driver/driver.component';
import $ from "jquery";
import { OperatorComponent } from './operator/operator.component';
import { UserService } from '../app/Servise/user.service'
import { DataHashService } from '../app/Servise/data-hash.service';
import { YamapngModule } from 'projects/yamapng/src/public_api';
import { YaCoreModule } from 'projects/yamapng/src/lib/core.module';
import { HttpService } from './Servise/http.service';
import { CookieService } from './Servise/cookies.service';
import { LkuserComponent } from './lkuser/lkuser.component';
import { LkdriverComponent } from './lkdriver/lkdriver.component';
import { InfoComponent } from './info/info.component';
import { ContactComponent } from './contact/contact.component';
import { PartnersComponent } from './partners/partners.component';
import { TarifComponent } from './tarif/tarif.component';
import { VacancyComponent } from './vacancy/vacancy.component';

import { AccessOperatorGuard } from './Guards/AccessOperator';
import { AccessUserGuard } from './Guards/AccessUser';
import { AccessVodGuard } from './Guards/AccessVod';
import { CanActivate } from '@angular/router/src/utils/preactivation';

const Routs: Routes = [
  {path: '', component: LoginComponent}, // авторизация
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent, canActivate: [AccessUserGuard]}, //главная страница пользователя (тут должна быть карта )
  {path: 'registr', component:RegistrComponent},//Регистрация
  {path: 'voditregistr', component:VoditregistrComponent, canActivate: [AccessVodGuard]}, //регистрация водителя
  {path: 'avtoregistr', component:AvtoregistrComponent, canActivate: [AccessVodGuard]}, // регистрация авто
  {path: 'driver', component:DriverComponent, canActivate: [AccessVodGuard]},   //главная страница драйвера
  {path: 'operator', component:OperatorComponent, canActivate: [AccessOperatorGuard]}, // главная страница оператора
  {path: 'lkuser',component:LkuserComponent, canActivate: [AccessUserGuard]}, //личный кабинет пользователя
  {path:'lkdriver', component:LkdriverComponent, canActivate: [AccessVodGuard]},// Личный кабинет водителя
  {path:'info', component:InfoComponent}, // информация о такси
  {path:'contacts', component:ContactComponent}, // Контакты кампании
  {path:'partners', component:PartnersComponent}, // Партнеры
  {path: 'tarif', component:TarifComponent}, // Тарифы
  



 ];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegistrComponent,
    VoditregistrComponent,
    AvtoregistrComponent,
    DriverComponent,
    OperatorComponent,
LkdriverComponent,
    LkuserComponent,

    InfoComponent,

    ContactComponent,

    PartnersComponent,

    TarifComponent,

    VacancyComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(Routs),
    HttpClientModule,
    BrowserModule,
    YamapngModule,
    YaCoreModule.forRoot({
      apiKey: ''}),
   

  ],
  providers: [
    CookieService,
    UserService,
    DataHashService,
    HttpService,
    AccessOperatorGuard,
    AccessUserGuard,
    AccessVodGuard,

  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
