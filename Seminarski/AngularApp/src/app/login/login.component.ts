import {Component, OnInit} from '@angular/core';
import {FormControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../helper/login-informacije";
import {MojConfig} from "../../MojConfig";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LogInComponent implements OnInit{
  LogInForma:FormGroup;
  submitted=false;
  success=false;
  korisnickoIme:any;
  lozinka:any;
  email:string;
  korisnikId:any;
  verifikacija:string;
  unesiKod: boolean=false;
  otvoriModal: boolean=false;
  promjena:boolean=false;
  novaLozinka: string;



  constructor(private formBuilder:FormBuilder, private router : Router, private httpKlijent:HttpClient) {

    this.LogInForma=this.formBuilder.group({
      korisnickoIme:new FormControl('', Validators.required),
      lozinka:new FormControl('', Validators.required)
    })
  }

  onSubmit()
  {
    this.submitted=true;

    if(this.LogInForma.invalid)
    {
      return;
    }

    let saljemo = {
      korisnickoIme:this.korisnickoIme,
      lozinka: this.lozinka
    };
    this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera + "/Autentifikacija/Login/", saljemo,MojConfig.http_opcije())
      .subscribe((x:LoginInformacije) =>{
        if (x.isLogiran) {
          porukaSuccess("Uspješna prijava!");
          AutentifikacijaHelper.setLoginInfo(x)
          this.router.navigate(['nekretnine/', 0]);
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null)
          porukaError("Prijava nije uspješna!");
        }
      });
  }

  ngOnInit (){
    this.korisnickoIme="";
    this.lozinka="";
  }

  posaljiKod() {
    let s={
      email:this.email
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Korisnik/posaljiVerifikacijskiKod", s).subscribe(x=>{
      this.korisnikId=x;
      porukaSuccess("Verifikacijski kod je poslan na email.");
      this.otvoriModal=false;
      this.unesiKod=true;
    })
  }

  provjeriValidnost(){
    let s={
      korisnikID:this.korisnikId,
      token: this.verifikacija
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Korisnik/ProvjeriValidnost", s).subscribe(x=>{
      if(x==true){
        porukaSuccess("Ispravan kod, unesite lozinku");
        this.unesiKod=false;
        this.promjena=true;
      }
      else
        porukaError("Neispravan kod.")
    })
  }

  NovaLozinka() {
    let s={
      id:this.korisnikId,
      novaLozinka:this.novaLozinka
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Korisnik/NovaLozinka", s).subscribe(x=>{
      porukaSuccess("Lozinka uspješno promijenjena.");
      this.promjena=false;
      this.router.navigateByUrl('/login');
    })
  }

}
