import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HttpClient} from '@angular/common/http'
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AppComponent} from "./app.component";
import { NavComponent } from './nav/nav.component';
import {RouterLink, RouterModule, RouterOutlet} from "@angular/router";
import { PomocComponent } from './pomoc/pomoc.component';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { KategorijaComponent } from './kategorija/kategorija.component';
import {RouterTestingModule} from "@angular/router/testing";
import {AppRoutingModule} from "./app-routing.module";
import {LogInComponent} from "./login/login.component";
import { NekretnineComponent } from './nekretnine/nekretnine.component';
import { KuceComponent } from './kuce/kuce.component';
import { StanoviComponent } from './stanovi/stanovi.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import {AutentifikacijaToken} from "./helper/login-informacije";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";
import { DetaljiNekretnineComponent } from './detalji-nekretnine/detalji-nekretnine.component';
import { AddNekretninaComponent } from './add-nekretnina/add-nekretnina.component';
import {NgMultiSelectDropDownModule} from "ng-multiselect-dropdown";
import { KorisnickiProfilComponent } from './korisnicki-profil/korisnicki-profil.component';
import { NekretnineVlasnikComponent } from './nekretnine-vlasnik/nekretnine-vlasnik.component';
import { RezervacijaComponent } from './rezervacija/rezervacija.component';
import { TranslateLoader, TranslateModule} from '@ngx-translate/core'
import { TranslateHttpLoader} from '@ngx-translate/http-loader';
import { SignalRComponent } from './signal-r/signal-r.component'

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PomocComponent,
    PocetnaComponent,
    KategorijaComponent,
    LogInComponent,
    NekretnineComponent,
    KuceComponent,
    StanoviComponent,
    RegistracijaComponent,
    DetaljiNekretnineComponent,
    AddNekretninaComponent,
    KorisnickiProfilComponent,
    NekretnineVlasnikComponent,
    RezervacijaComponent,
    SignalRComponent,

  ],
  exports: [RouterModule],
  bootstrap:[AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterOutlet,
    RouterLink,
    RouterTestingModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    })
  ],
  providers: [],
})
export class AppModule { }
export function httpTranslateLoader(http: HttpClient){
  return new TranslateHttpLoader(http);
}




