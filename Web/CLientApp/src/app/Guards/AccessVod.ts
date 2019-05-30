import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import {Observable} from 'rxjs';
import { Injectable } from '@angular/core';
import { UserService } from '../Servise/user.service';


@Injectable()
export class AccessVodGuard implements CanActivate {

    constructor(
        private router: Router,
        private user: UserService
      ) { }

      canActivate() {
        const model =  this.user.GetCurrentUser();
        if (model.roleId === 3) {
            return true;
        } else {
            this.router.navigate(['']);
            return false; }
    }

}