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
import { DriverComponent } from './driver/driver.component';
import $ from "jquery";
import { OperatorComponent } from './operator/operator.component';
import { UserService } from '../app/Servise/user.service'
import { DataHashService } from '../app/Servise/data-hash.service';
import { MapComponent } from './map/map.component';
import { YamapngModule } from 'projects/yamapng/src/public_api';
import { YaCoreModule } from 'projects/yamapng/src/lib/core.module';

 

const Routs: Routes = [
  {path: '', component: LoginComponent}, // авторизация
  {path: 'home', component: HomeComponent}, //главная страница пользователя (тут должна быть карта )
  {path: 'registr', component:RegistrComponent},//Регистрация
  {path: 'voditregistr', component:VoditregistrComponent}, //регистрация водителя
  {path: 'avtoregistr', component:AvtoregistrComponent}, // регистрация авто
  {path: 'driver', component:DriverComponent},   //главная страница драйвера
  {path: 'operator', component:OperatorComponent}, // главная страница оператора
  {path:'map', component:MapComponent}       //просто добавленная карта



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
    MapComponent,
    
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
    
   // UserService,
  //  DataHashService,


  ],
  bootstrap: [AppComponent]
})
export class AppModule {}