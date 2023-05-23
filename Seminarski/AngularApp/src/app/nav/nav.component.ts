import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../MojConfig";
import {TranslateService} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";

declare function porukaSuccess(a: string):any;
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent {
  constructor(private autentifikacijahelper: AutentifikacijaHelper, private router: Router,private httpKlijent:HttpClient,public translate: TranslateService) {
      translate.addLangs(['bos','en']);
      translate.setDefaultLang('bos');
  }
  switchLang(lang: string){
    this.translate.use(lang);
  }
  appTitle: string = 'OpenDoors';

  loginInfo():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  logOut() {
    let token=MojConfig.http_opcije();
    AutentifikacijaHelper.setLoginInfo(null);

    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/LogOut/", null, token)
      .subscribe((x: any) => {
        porukaSuccess("Logout uspješan.");
      });

    this.router.navigateByUrl("/login");
  }
}
