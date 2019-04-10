import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';
import { HttpService } from '../Servise/http.service';
import { CookieService } from 'ngx-cookie-service';
import { UserService } from '../Servise/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private route: Router, private http: HttpService, /*private cookieService: CookieService, private user:UserService */) { }

  ngOnInit() {
  }
  xer(Form: NgForm) {
    console.log(Form.value);
    this.http.post('account/login', Form.value).subscribe  ((data:any) => {
    //  this.user.SetIdentity(data);
      if (data.roleId == 2) {this.route.navigateByUrl('home');}
     else {this.route.navigateByUrl('driver');}
    }, err => {      alert('Ошибка');
    });
  }

}
