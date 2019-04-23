import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-avtoregistr',
  templateUrl: './avtoregistr.component.html',
  styleUrls: ['./avtoregistr.component.css']
})
export class AvtoregistrComponent implements OnInit {
CarBrand;
  constructor(private route: Router, private http: HttpService) { }

  ngOnInit() { 
    this.Brand()
  }
  
  Brand() {
    this.http.get('CarBrand/Get').subscribe(data => {
      this.CarBrand = data;
    console.log(data)
})
}}