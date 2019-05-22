import { Component, OnInit } from '@angular/core';
import { UserService } from '../Servise/user.service';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.css']
})
export class DriverComponent implements OnInit {

  Driver;
  constructor(private user: UserService) { }

  ngOnInit() {
    this.Driver = this.user.GetCurrentUser();
  }

}
