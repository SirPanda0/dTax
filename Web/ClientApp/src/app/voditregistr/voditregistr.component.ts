import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-voditregistr',
  templateUrl: './voditregistr.component.html',
  styleUrls: ['./voditregistr.component.css']
})

export class VoditregistrComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  Vodit(Form: NgForm) {
    console.log(Form.value);
    this.http.post('account/login', Form.value).subscribe(data => {
      this.route.navigateByUrl('home');
    }, err => {      alert('Ошибка');
    });
  }
}

