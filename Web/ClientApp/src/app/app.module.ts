import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CookieService } from 'ngx-cookie-service';

import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { RegistrComponent } from './registr/registr.component';
import { VoditregistrComponent } from './voditregistr/voditregistr.component';
import { AvtoregistrComponent } from './avtoregistr/avtoregistr.component';
import { OperatorComponent } from './operator/operator.component';
import { DriverComponent } from './driver/driver.component';
import * as $ from 'jquery';


const Routs: Routes = [
  {path: '', component: LoginComponent}, // авторизация
  {path: 'home', component: HomeComponent}, //главная страница пользователя
  {path: 'registr', component:RegistrComponent},//Регистрация
  {path: 'voditregistr', component:VoditregistrComponent}, //регистрация водителя
  {path: 'avtoregistr', component:AvtoregistrComponent}, // регистрация авто
  {path: 'operator', component:OperatorComponent}, //главная страница оператора
  {path: 'driver', component:DriverComponent}   //главная страница драйвера



 ];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegistrComponent,
    VoditregistrComponent,
    AvtoregistrComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(Routs),
    HttpClientModule,

  ],
  providers: [
    [ CookieService ]
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}