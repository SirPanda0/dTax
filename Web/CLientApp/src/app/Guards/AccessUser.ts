import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import {Observable} from 'rxjs';
import { Injectable } from '@angular/core';
import { UserService } from '../Servise/user.service';


@Injectable()
export class AccessUserGuard implements CanActivate {

    constructor(
        private router: Router,
        private user: UserService
      ) { }

      canActivate() {
        const model =  this.user.GetCurrentUser();
        if (model.roleId === 2) {
            return true;
        } else {
            this.router.navigate(['']);
            return false; }
    }

}