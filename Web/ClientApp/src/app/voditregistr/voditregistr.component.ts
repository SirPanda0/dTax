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

  constructor(private route: Router, private http: HttpService,) { }

  ngOnInit() {
  }
  regg(Form: NgForm) {
    console.log(Form.value);
    this.http.post('Drivers/Register', Form.value ).subscribe(data => {
     {this.route.navigateByUrl('avtoregistr');
  }

    }, err => {
      alert('Ошибка регистрации, проверьте правильность данных');
    });
}
}
