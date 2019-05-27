import { Component, OnInit } from '@angular/core';
import { UserService } from '../Servise/user.service';
import { HttpService } from '../Servise/http.service';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.css']
})
export class DriverComponent implements OnInit {

  Driver;
  Orders;
  constructor(private user: UserService, private http: HttpService) { }

  ngOnInit() {
    this.Driver = this.user.GetCurrentUser();
    this.GetOrder();
  }

  GetOrder() {
    this.http.get('CabRide/GetRideList?page=0').subscribe( data => {
      this.Orders = data;
      console.log(data);
    });
  }
}
