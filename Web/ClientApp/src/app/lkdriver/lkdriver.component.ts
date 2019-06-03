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
  constructor(private http: HttpService) { }

  ngOnInit() {
  }

  ChangePhotoCar(event) {
    this.PhotoCar = event;
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

}
