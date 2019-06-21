import { Component, OnInit } from '@angular/core';
import { UserService } from '../Servise/user.service';
import { HttpService } from '../Servise/http.service';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.css']
})
export class DriverComponent implements OnInit {

  Driver: any;
  Orders: any;
  MyOrders: any = {};
  constructor(private user: UserService, private http: HttpService) { }

  ngOnInit() {
    this.Driver = this.user.GetCurrentUser();
    this.GetOrder();
  }

  GetOrder() {
    this.http.get('CabRide/GetRideList?page=1').subscribe( (data: any) => {
      this.Orders = data;
      this.GetActiveOrder();

    });
  }
  TakeOrder(id) {
    this.http.get('CabRide/TakeOrder?id=' + id).subscribe(data => {
      this.GetOrder();
      alert('Заказ принят');
    }, erroe => {
      alert('Заказ принят');
    });
  }

  GetActiveOrder() {
    this.http.get('CabRide/GetActiveDriverRide').subscribe((data: any) => {
     this.MyOrders = data;
     console.log(data)
    });
  }

  StartMyOrder(id) {
    this.http.get('CabRide/StartRide?id=' + id).subscribe(data => {
      alert('Поездка началась');
      this.GetActiveOrder();
    });
  }
  EndtMyOrder(id) {
    this.http.get('CabRide/EndRide?id=' + id).subscribe(data => {
      alert('Поездка завершена');
      this.GetActiveOrder();
    });
  }
}
