import {Component, Input, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {NekretninaVM} from "./nekretninaVM";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";


declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-add-nekretnina',
  templateUrl: './add-nekretnina.component.html',
  styleUrls: ['./add-nekretnina.component.css']
})
export class AddNekretninaComponent implements OnInit {
  pogodnostiNekretnine: any;
  novaNekretnina: NekretninaVM;
  lokacije: any;
  tipovi: any;

  @Input() urediNekretninu: any;
   slikeUredjeneNekretnine: any;

  constructor(private httpKlijent: HttpClient, private router: Router) {

  }

  ngOnInit() {
    this.fetchPogodnosti();
    this.fetchTipove();
    this.fetchLokacije();
    if (this.urediNekretninu != null){
      this.fetchSlikeString();
      this.novaNekretnina = this.urediNekretninu;
    }
    else {
      this.novaNekretnina = {
        id: 0,
        brojKvadrata: 0,
        brojSoba: 0,
        brojKupatila: 0,
        brojKreveta: 0,
        cijenaPoDanu: 0,
        adresa: " ",
        avans: false,
        lokacijaId: 0,
        tipId: 1,
        tip: " ",
        vlasnikId: this.loginInfo().autentifikacijaToken.korisnickiNalog.id,
        slike: [],
        selectedPogodnosti: [],
      };
    }
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  fetchPogodnosti(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/PogodnostiNekretnine/GetAll`, MojConfig.http_opcije()).subscribe(data => {
      this.pogodnostiNekretnine = data;
    });
  }

  fetchLokacije(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Lokacija/GetAll`, MojConfig.http_opcije()).subscribe(data => {
      this.lokacije = data;
    });
  }

  fetchTipove(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/TipNekretnine/GetAll`, MojConfig.http_opcije()).subscribe(data => {
      this.tipovi = data;
    });
  }

  fetchSlikeString(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Slike/GetSlikaString?nekretnina_id=`+this.urediNekretninu.id, MojConfig.http_opcije()).subscribe(data => {
      this.slikeUredjeneNekretnine = data;
    });
  }

  addNekretnina() {
    this.novaNekretnina.selectedPogodnosti = this.pogodnostiNekretnine.filter((w: any) => w.selected == true).map((s: any) => s.id);
    if(this.urediNekretninu!=null)
      this.novaNekretnina.slike=this.slikeUredjeneNekretnine.map((x:any)=>x.slikaString);
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Snimi`, this.novaNekretnina, MojConfig.http_opcije()).subscribe(x => {
      this.router.navigate(['nekretnine-vlasnik/', this.loginInfo().autentifikacijaToken.korisnickiNalogId])
      if(this.urediNekretninu==null)
        porukaSuccess("Uspješno dodata nekretnina!")
      else
        porukaSuccess("Uspješno spašene promjene!")
    });
  }

  postaviNekretninu() {
    this.addNekretnina();
  }

  promjenaAvansa(event: any) {
    if (event.target.checked)
      this.novaNekretnina.avans = true;
    else
      this.novaNekretnina.avans = false;
  }


  randomIntFromInterval(min: number, max: number) { // min and max included
    return Math.floor(Math.random() * (max - min + 1) + min)
  }
  get_slika(s: string) {
    return s;
  }

  generisi_preview() {
    // @ts-ignore
    let files = document.getElementById("slika-input").files;
    if (files.length > 0) {
      for (let file of files) {
        let reader = new FileReader();
        let this2 = this;
        reader.onload = function () {
          this2.novaNekretnina.slike.push(reader.result.toString());
        }
        reader.readAsDataURL(file);
      }

    }
  }

  // @ts-ignore
  checkirajPogodnost() {
      for (let p of this.urediNekretninu.selectedPogodnosti)
        return p.id;
  }
}
