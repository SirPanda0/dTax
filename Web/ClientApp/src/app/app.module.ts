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

 

const Routs: Routes = [
  {path: '', component: LoginComponent}, // авторизация
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent}, //главная страница пользователя (тут должна быть карта )
  {path: 'registr', component:RegistrComponent},//Регистрация
  {path: 'voditregistr', component:VoditregistrComponent}, //регистрация водителя
  {path: 'avtoregistr', component:AvtoregistrComponent}, // регистрация авто
  {path: 'driver', component:DriverComponent},   //главная страница драйвера
  {path: 'operator', component:OperatorComponent}, // главная страница оператора
  {path: 'lkuser',component:LkuserComponent}, //личный кабинет пользователя
  {path:'lkdriver', component:LkdriverComponent},// Личный кабинет водителя
  {path:'info', component:InfoComponent}, // информация о такси
  {path:'contacts', component:ContactComponent}, // Контакты кампании
  {path:'partners', component:PartnersComponent}, // Партнеры
  



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

  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
