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
    this.http.post('account/register', Form.value).subscribe(data => {
      this.route.navigateByUrl('home');
    }, err => {
      alert('Хрень');
    });
  }

}
