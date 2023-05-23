import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../../MojConfig";
import {SignalRComponent} from "../signal-r/signal-r.component";

declare function porukaSuccess(a: string): any;

declare function porukaError(a: string): any;

@Component({
  selector: 'app-rezervacija',
  templateUrl: './rezervacija.component.html',
  styleUrls: ['./rezervacija.component.css']
})
export class RezervacijaComponent {
  korisnikId: number;
  kreditnaKarticaPodaci: any;
   errors: any;

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  nekretnina: number;
  register: FormGroup;
  uplata: FormGroup;
  tipoviKartice = [{naziv: 'Kreditna'},
    {naziv: 'Debitna'}];

  minDate: string;
  constructor(private httpKlijent: HttpClient, private router: Router, private route: ActivatedRoute,
              private formBuilder: FormBuilder, public  signalRServis: SignalRComponent) {
    const today = new Date();
    today.setDate(today.getDate() + 1);
    this.minDate = today.toISOString().split('T')[0];
  }


  ngOnInit(): void {
    this.route.params.subscribe(s => {
      this.nekretnina = +s['id']
    });
    this.korisnikId = this.loginInfo().autentifikacijaToken.korisnickiNalogId;
    this.fetchKreditneKartice();

    this.register = this.formBuilder.group({
      brojOsoba: new FormControl('', [
        Validators.required,
      ]),
      djeca: new FormControl('', [
        Validators.required,
      ]),
      checkIn: new FormControl('', [
        Validators.required,
      ]),
      checkOut: new FormControl('', [
        Validators.required,
      ]),
      kreditnaKarticaId: new FormControl('', [
        Validators.required,
      ]),
      nekretninaId: new FormControl(this.nekretnina),
      korisnikId: new FormControl(this.korisnikId)
    }, {
      validator: this.dateRangeValidator('checkIn', 'checkOut')
    });
  }

  dateRangeValidator(startDateControlName: string, endDateControlName: string) {
    return (formGroup: FormGroup) => {
      const checkIn = formGroup.controls[startDateControlName];
      const checkOut = formGroup.controls[endDateControlName];
      if (checkIn.value && checkOut.value && checkIn.value > checkOut.value) {
        checkOut.setErrors({ 'minDate': true });
      } else {
        checkOut.setErrors(null);
      }
    }
  }
  get brojOsoba(): FormControl {
    return this.register.get("brojOsoba") as FormControl;
  }

  get djeca(): FormControl {
    return this.register.get("djeca") as FormControl;
  }

  get checkIn(): FormControl {
    return this.register.get("checkIn") as FormControl;
  }

  get checkOut(): FormControl {
    return this.register.get("checkOut") as FormControl;
  }
  get kreditnaKarticaId(): FormControl {
    return this.register.get("kreditnaKarticaId") as FormControl;
  }


  collect() {
    if (this.register.valid) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Rezervacija/Snimi`, this.register.value, MojConfig.http_opcije()).subscribe(x => {
        porukaSuccess('Rezervacija uspjesna!');
        this.router.navigate(["korisnicki-profil/"]);
        this.signalRServis.zapocniKonekcijuRezervacije(this.nekretnina);
      }, error => {porukaError("Nekretnina je zauzeta u izabranom periodu.")});
    } else
      console.table(this.register.value);
  }

  fetchKreditneKartice() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/KreditnaKartica/GetById?korisnikId=" + this.korisnikId).subscribe((x: any) => {
      this.kreditnaKarticaPodaci = x;
    })
  }

  otvoriModal() {
    this.uplata = this.formBuilder.group({
      datumIsteka: new FormControl('', [
        Validators.required,
      ]),
      CVV: new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]{3}'),
      ]),
      brojKartice: new FormControl('', [
        Validators.required,
        Validators.minLength(12),
        Validators.maxLength(16),
      ]),
      tipKartice: new FormControl('', [
        Validators.required]),
      korisnikId: new FormControl(this.korisnikId)
    });
  }

  spasiKarticu() {
    if (this.uplata.valid) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/KreditnaKartica/Snimi`, this.uplata.value, MojConfig.http_opcije()).subscribe(x => {
        this.fetchKreditneKartice();
        porukaSuccess('Unos kartice uspjesan!');
        this.uplata=null;
      });
    } else
      porukaError('Unos kartice neuspjesan!');
  }

  get brojKartice(): FormControl {
    return this.uplata.get("brojKartice") as FormControl;
  }

  get datumIsteka(): FormControl {
    return this.uplata.get("datumIsteka") as FormControl;
  }

  get CVV(): FormControl {
    return this.uplata.get("CVV") as FormControl;
  }

  get tipKartice(): FormControl {
    return this.uplata.get("tipKartice") as FormControl;
  }
}
