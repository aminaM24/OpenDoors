import { Component } from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Route, Router, RouterLinkActive, RouterModule} from "@angular/router";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-nekretnine-vlasnik',
  templateUrl: './nekretnine-vlasnik.component.html',
  styleUrls: ['./nekretnine-vlasnik.component.css']
})
export class NekretnineVlasnikComponent {

  constructor(private httpKlijent: HttpClient, private router: Router, private route: ActivatedRoute) {
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  vlasnikId :number;
  nekretninaVlasnikPodaci : any;
  ngOnInit(): void {
    this.route.params.subscribe(s=>{
      this.vlasnikId=+s['id'];
    })
    this.fetchVlasnikNekretnine();
  }

  fetchVlasnikNekretnine() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Nekretnina/GetByVlasnik?korisnickiNalogId=" + this.vlasnikId,MojConfig.http_opcije()).subscribe(x=>{
      this.nekretninaVlasnikPodaci = x;
    });
  }

  detaljiNekretnine(s:any) {
    this.router.navigate(["detalji-nekretnine/",s.id])
  }
  delete(s: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Delete/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchVlasnikNekretnine();
    });
  }
  getslika(slika_id: any) {
    return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${slika_id}`;
  }
}
