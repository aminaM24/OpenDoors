import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {Injectable} from "@angular/core";

declare function porukaError(a: string):any;

@Injectable()
export class VlasnikPermisije implements CanActivate {
  constructor(private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    if(AutentifikacijaHelper.getLoginInfo().isLogiran &&
      AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.isVlasnik) {
      return true;
    }
    else {
      porukaError('Nemate permisije!');
      this.router.navigateByUrl('/login');
      AutentifikacijaHelper.setLoginInfo(null);
      return false;
    }
  }
}
