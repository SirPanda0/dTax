import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-lkdriver',
  templateUrl: './lkdriver.component.html',
  styleUrls: ['./lkdriver.component.css']
})
export class LkdriverComponent implements OnInit {

  PhotoCar: any;
  PhotoVod: any;
  constructor(private http: HttpService) { }

  ngOnInit() {
  }

  ChangePhotoCar(event) {
    this.PhotoCar = event;
  }

  ChangePhotoVod(event) {
    this.PhotoVod = event;
  }

  LoadPhotoCar() {
    this.http.makeFileRequest('FileStorage/Upload', this.PhotoCar).then( data => {
      this.http.get('Cab/FileToCab?FileId=' + data).subscribe( data1 => {
        alert('Фото добавлено');
      }, error1 => {
        alert('Добавить неудалось');
      });
    });
  }

  LoadPhotoVod() {
    this.http.makeFileRequest('FileStorage/Upload', this.PhotoVod).then( data => {
      this.http.get('Drivers/FileToDriver?FileId=' + data).subscribe( data1 => {
        alert('Фото добавлено');
      }, error1 => {
        alert('Добавить неудалось');
      });
    });
  }
}
