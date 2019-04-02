import { Injectable } from '@angular/core';
import { CookieService } from '../Servise/cookies.service';
import { DataHashService } from './data-hash.service';
import { UserModel } from '../login/UserModel';
import { HttpService } from './http.service';

const USERCOOKIEKEY = 'user';

@Injectable()
export class UserService {
  constructor(
    private cookie: CookieService,
    private dataSeq: DataHashService,
    private http: HttpService
  ) {}

  public SetIdentity(model: UserModel) {
    this.cookie.setCookie(USERCOOKIEKEY, this.dataSeq.EncodeJSON(model), 10);
  }

  public UpdateUserInfo(model) {
    this.cookie.setCookie(USERCOOKIEKEY, this.dataSeq.EncodeJSON(model), 10);
  }

  public GetCurrentUser(): UserModel {

    return this.dataSeq.DecodeJSON(this.cookie.getCookie(USERCOOKIEKEY));
  }

 // public IsAuth(): boolean {
 //   if (!(this.GetCurrentUser().operatorID)) {
//      return false;
 //   }
 //   return true;
 // }

  public LogOut() {
    this.cookie.deleteCookie(USERCOOKIEKEY);
    this.http.get('accounts/Logout').subscribe();
  }
}