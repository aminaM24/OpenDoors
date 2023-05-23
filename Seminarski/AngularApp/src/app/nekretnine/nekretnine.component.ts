import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {MojConfig} from "../../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-nekretnine',
  templateUrl: './nekretnine.component.html',
  styleUrls: ['./nekretnine.component.css']
})
export class NekretnineComponent implements OnInit {
  nekretninaPodaci: any;
  Id: any;


  constructor(private httpKlijent: HttpClient, private router: Router, private route: ActivatedRoute) {
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.route.params.subscribe(s => {
      this.Id = +s["id"];
    });
    this.getNekretnine();
  }

  getNekretnine(): void {
    if (this.Id == 0) {
      if (this.loginInfo().isLogiran && this.loginInfo().autentifikacijaToken.korisnickiNalog.isVlasnik)
        this.httpKlijent.get(MojConfig.adresa_servera + "/Nekretnina/GetAllOsimVlasnika?korisnickiNalogId=" + this.loginInfo().autentifikacijaToken.korisnickiNalog.id, MojConfig.http_opcije()).subscribe(x => {
          this.nekretninaPodaci = x;
        })
      else
        this.httpKlijent.get(MojConfig.adresa_servera + "/Nekretnina/GetAll", MojConfig.http_opcije()).subscribe(x => {
          this.nekretninaPodaci = x;
        })
    } else {
      if (this.loginInfo().isLogiran && this.loginInfo().autentifikacijaToken.korisnickiNalog.isVlasnik)
        this.httpKlijent.get(MojConfig.adresa_servera + "/Nekretnina/GetByTipOsimVlasnika?tipId=" + this.Id + "&korisnickiNalogId=" + this.loginInfo().autentifikacijaToken.korisnickiNalog.id, MojConfig.http_opcije()).subscribe(x => {
          this.nekretninaPodaci = x;
        })
      else
        this.httpKlijent.get(`${MojConfig.adresa_servera}/Nekretnina/GetByTip?tipId=` + this.Id, MojConfig.http_opcije()).subscribe(x => {
          this.nekretninaPodaci = x;
        });
    }
  }

  detaljiNekretnine(s: any) {
    this.router.navigate(["detalji-nekretnine/", s.id])
  }

  delete(s: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Delete/${s.id}`, MojConfig.http_opcije()).subscribe(x => {
      this.getNekretnine();
    });
  }

  getslika(slika_id: any) {
    return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${slika_id}`;
  }
}
