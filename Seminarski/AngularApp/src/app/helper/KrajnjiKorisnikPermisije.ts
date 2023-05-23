import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {Injectable} from "@angular/core";
declare function porukaError(a: string):any;

@Injectable()
export class ProvjeraAktivacija implements CanActivate {
  constructor(private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    if(AutentifikacijaHelper.getLoginInfo().isLogiran) {
      return true;
    }
    else {
      porukaError('Morate se prvo logirati!');
      this.router.navigateByUrl('/login');
      return false;
    }
  }
}
