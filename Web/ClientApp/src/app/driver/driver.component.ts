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
  Orders: any;
  MyOrders: any;
  constructor(private user: UserService, private http: HttpService) { }

  ngOnInit() {
    this.Driver = this.user.GetCurrentUser();
    this.GetOrder();
  }

  GetOrder() {
    this.http.get('CabRide/GetRideList?page=0').subscribe( (data: any) => {
      this.Orders = data.collection;

    });
  }
  TakeOrder(id) {
    this.http.post('CabRide/TakeOrder', id).subscribe(data => {
      this.GetOrder();
      alert('Заказ принят');
    });
  }
}
