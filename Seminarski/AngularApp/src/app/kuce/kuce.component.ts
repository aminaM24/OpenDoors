import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-kuce',
  templateUrl: './kuce.component.html',
  styleUrls: ['./kuce.component.css']
})
export class KuceComponent implements OnInit{
  kucePodaci: any;
  constructor(private httpKlijent: HttpClient, private router: Router) {
  }
  fetchKuce(s: any) {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Nekretnina/GetByTip?tipId=`+1,MojConfig.http_opcije()).subscribe(x=>{
      this.kucePodaci=x;
    });
  }

  ngOnInit(): void {
    this.fetchKuce(1);
  }

  detaljiNekretnine(s:any) {
    this.router.navigate(["detalji-nekretnine/",s.id])
  }

  getslika(slika_id: any) {
    return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${slika_id}`;
  }

  delete(s: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Delete/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchKuce(1);
    });
  }
}
