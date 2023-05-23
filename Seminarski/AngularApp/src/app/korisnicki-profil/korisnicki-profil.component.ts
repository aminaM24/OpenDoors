import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {NavComponent} from "../nav/nav.component";
import {SignalRComponent} from "../signal-r/signal-r.component";

declare function porukaSuccess(a: string): any;

declare function porukaError(a: string): any;

@Component({
  selector: 'app-korisnicki-profil',
  templateUrl: './korisnicki-profil.component.html',
  styleUrls: ['./korisnicki-profil.component.css'],
  providers: [NavComponent]
})
export class KorisnickiProfilComponent  implements OnInit{

  constructor(private httpKlijent: HttpClient, private router :Router, private odjava:NavComponent, public  signalRServis: SignalRComponent) {
  }
  rezervacije: any;
  verifikacijskiKod: any;
  unesiKod: boolean=false;
  korisnikId: any;
  novaSifra:any;
  promjenaLozinke=false;
  promjenaSlike: boolean;
  novaSlika:any;
  currentphoto=this.getSlikuKorisnika(this.loginInfo().autentifikacijaToken.korisnickiNalogId);
  clock_tick=Date.now();
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  email=this.loginInfo().autentifikacijaToken.korisnickiNalog.email;
  ngOnInit(): void {
    this.novaSifra={id:this.loginInfo().autentifikacijaToken.korisnickiNalog.id,
      staraLozinka:"", novaLozinka:""};
    //console.log(this.loginInfo().autentifikacijaToken.korisnickiNalog.email);
  }

  PromijeniPassword() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/PromijeniLozinku`, this.novaSifra, MojConfig.http_opcije()).subscribe(x=>{
      this.promjenaLozinke=false;
      AutentifikacijaHelper.setLoginInfo(x=null);
      this.router.navigateByUrl("/login");
      porukaSuccess("Uspješno ste promjenili lozinku. Molimo vas, ponovo se prijavite.")
    });
  }

  otvoriNekretnineVlasnika(s:any){
    this.router.navigate(['nekretnine-vlasnik/', s]);
  }

  otvoriRezervacijeKorisnika(id: number) {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Rezervacija/GetByKorisnikId?korisnikId=" + id,MojConfig.http_opcije()).subscribe(x=>{
      this.rezervacije = x;
    });
  }

  otkaziRezervaciju(s:any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Rezervacija/OtkaziRezervaciju/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.otvoriRezervacijeKorisnika(this.loginInfo().autentifikacijaToken.korisnickiNalogId);
      this.signalRServis.zapocniKonekcijuRezervacije(s.nekretninaId);
    });
  }

  getSlikuKorisnika(korisnickiNalogId: number) {
    return `${MojConfig.adresa_servera}/Korisnik/GetSlikuKorisnika/${korisnickiNalogId}`;
  }

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("formFile").files[0];
    if (file) {
      var reader = new FileReader();
      let this2=this;
      reader.onload=function ()
      {
        this2.novaSlika.slikaKorisnika=reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  promijeniSliku() {
    this.novaSlika={id:this.loginInfo().autentifikacijaToken.korisnickiNalogId, slikaKorisnika:""};
    this.promjenaSlike=true;
  }

  spasiNovuSliku() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/PromijeniSliku`, this.novaSlika, MojConfig.http_opcije()).subscribe(x=>{
      this.promjenaSlike=false;
      porukaSuccess("Uspješno ste promijenili profilnu sliku!");
      this.currentphoto=this.getSlikuKorisnika(this.loginInfo().autentifikacijaToken.korisnickiNalogId);
      this.clock_tick=Date.now();
    });
  }


  verifikacijaProfila() {
    let s={
      email:this.email
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Korisnik/posaljiVerifikacijskiKod", s).subscribe(x=>{
      this.korisnikId=x;
      porukaSuccess("Verifikacijski kod je poslan na email.");
      this.unesiKod=true;
    })
  }

  verifikuj() {
    let podaci={
      korisnikID:this.korisnikId,
      token:this.verifikacijskiKod
    }
    this.httpKlijent.post(MojConfig.adresa_servera + '/Korisnik/Verifikuj',podaci).subscribe(response=>{
      if(response==true){
        porukaSuccess('Verifikacija uspješna!');
        this.unesiKod=false;
      }
      else
        porukaError('Verifikacijski kod nije ispravan, molimo vas pokušajte ponovo!');
    })
  }

}
