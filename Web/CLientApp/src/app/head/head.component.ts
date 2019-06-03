import { Component, OnInit } from '@angular/core';
import { UserService } from '../Servise/user.service';
import { HttpService } from '../Servise/http.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.css']
})
export class HeadComponent implements OnInit {

  User;
  constructor(private user: UserService, private http: HttpService, private route: Router) { }

  ngOnInit() {
    this.User = this.user.GetCurrentUser();
  }

  Exit() {
    this.http.get('Account/Logout').subscribe( data => {
      this.user.SetIdentity({});
      this.User = null;
      this.route.navigateByUrl('');
    });
  }
}
