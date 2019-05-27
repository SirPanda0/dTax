import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';

@Component({
  selector: 'app-operator',
  templateUrl: './operator.component.html',
  styleUrls: ['./operator.component.css']
})
export class OperatorComponent implements OnInit {

  Confirm;
  UnConfirm;
  constructor(private http: HttpService) { }

  ngOnInit() {
    this.GetDriverConfirm();
    
  }

  GetDriverConfirm() {
    this.http.get('Operator/GetList?page=0').subscribe( (data: any) => {
      this.Confirm = data.collection;
      this.GetUnconfirmedList();
    })
  }

  GetUnconfirmedList() {
    this.http.get('Operator/GetUnconfirmedList?page=1').subscribe( (data: any) => {
      this.UnConfirm = data.collection;
      console.log(this.UnConfirm);
    })
  }
  Privaz(id) {
    this.http.get('Operator/ConfirmDriver?DriverId=' + id).subscribe(data => {
      this.UnConfirm = null;
      this.GetDriverConfirm();
    });
  }
  Otvaz(id) {
    this.http.get('Operator/UnconfirmDriver?DriverId=' + id).subscribe(data => {
      this.Confirm = null;
      this.GetDriverConfirm();
    });
  }
}
