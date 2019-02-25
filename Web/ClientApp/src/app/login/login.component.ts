import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';
import { HttpService } from '../Servise/http.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private route: Router, private http: HttpService) { }

  ngOnInit() {
  }
  xer(Form: NgForm) {
    console.log(Form.value);
    this.http.post('account/login', Form.value).subscribe(data => {
      this.route.navigateByUrl('home');
    }, err => {
      alert('Хрень');
    });
  }

}
