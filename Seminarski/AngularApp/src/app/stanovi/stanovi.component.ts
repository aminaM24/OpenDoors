import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-stanovi',
  templateUrl: './stanovi.component.html',
  styleUrls: ['./stanovi.component.css']
})
export class StanoviComponent implements OnInit{
  stanoviPodaci: any;
  constructor(private httpKlijent: HttpClient, private router: Router) {
  }
  fetchStanove(s: any) {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Nekretnina/GetByTip?tipId=`+2,MojConfig.http_opcije()).subscribe(x=>{
      this.stanoviPodaci=x;
    });
  }

  ngOnInit(): void {
    this.fetchStanove(2);
  }

  detaljiNekretnine(s:any) {
    this.router.navigate(["detalji-nekretnine/",s.id])
  }

  getslika(slika_id: any) {
    return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${slika_id}`;
  }

  delete(s: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Delete/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchStanove(2);
    });
  }
}
