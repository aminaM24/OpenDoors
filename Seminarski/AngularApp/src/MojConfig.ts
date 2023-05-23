import {AutentifikacijaToken} from "./app/helper/login-informacije";
import {AutentifikacijaHelper} from "./app/helper/autentifikacija-helper";

export class MojConfig{
  static adresa_servera = "https://localhost:7115";
  static http_opcije= function (){

    let autentifikacijaToken:AutentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if (autentifikacijaToken!=null)
      mojtoken = autentifikacijaToken.vrijednost;
    return {
      headers: {
        'autentifikacija-token': mojtoken,
      }
    };
  }
}
