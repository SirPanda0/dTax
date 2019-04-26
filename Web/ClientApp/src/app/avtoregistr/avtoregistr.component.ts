import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';
import { UserService } from '../Servise/user.service';

@Component({
  selector: 'app-avtoregistr',
  templateUrl: './avtoregistr.component.html',
  styleUrls: ['./avtoregistr.component.css']
})
export class AvtoregistrComponent implements OnInit {

  IdBrand;
  CarBrand;

  ModelAvto;
  IdModel;

  TypeAvto;
  IdType;

  ColorAvto;
  IdColor;


  constructor(private route: Router, private http: HttpService, private user: UserService) { }

  ngOnInit() {
    this.GetBrand();
    this.GetType();
    this.GetColor();
  }

  GetBrand() {
      this.http.get('CarBrand/Get').subscribe(data => {
        this.CarBrand = data;
    });
  }

  GetModel(id) {
    this.ModelAvto = null;
    this.http.get('CarModel/GetModelsByBrand?brandId=' + id).subscribe(data => {
      this.ModelAvto = data;
  });
  }

  GetType() {
      this.http.get('CarType/Get').subscribe(data => {
        this.TypeAvto = data;
    });
  }

  GetColor() {
    this.http.get('CarColor/Get').subscribe(data => {
      this.ColorAvto = data;
  });
}

  SendAvto(Form: NgForm) {
    const driver = this.user.GetCurrentUser();
    // tslint:disable-next-line:no-string-literal
    Form.value['DriverId'] = driver.id;
    this.http.post('Cab/Add', Form.value).subscribe(data => {

    });
  }

}
