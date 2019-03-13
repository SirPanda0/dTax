import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-registr',
  templateUrl: './registr.component.html',
  styleUrls: ['./registr.component.css']
})
export class RegistrComponent implements OnInit {

  constructor(private route: Router, private http: HttpService) { }

  ngOnInit() {

  }
  reg(Form: NgForm) {
    console.log(Form.value);
    this.http.post('account/register', Form.value ).subscribe(data => {
      if (Form.value['roleId'] == 2) {this.route.navigateByUrl('home');} 
      else  {this.route.navigateByUrl('voditregistr');}
      
    }, err => {
      alert('Ошибка регистрации, проверьте правильность данных');
    });
  }

}
