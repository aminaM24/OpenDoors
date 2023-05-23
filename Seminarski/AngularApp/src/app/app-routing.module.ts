import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { PomocComponent } from './pomoc/pomoc.component';
import { KategorijaComponent } from './kategorija/kategorija.component';
import {CommonModule} from "@angular/common";
import {LogInComponent} from "./login/login.component";
import {NekretnineComponent} from "./nekretnine/nekretnine.component";
import {KuceComponent} from "./kuce/kuce.component";
import {StanoviComponent} from "./stanovi/stanovi.component";
import {RegistracijaComponent} from "./registracija/registracija.component";
import {DetaljiNekretnineComponent} from "./detalji-nekretnine/detalji-nekretnine.component";
import {AddNekretninaComponent} from "./add-nekretnina/add-nekretnina.component";
import {KorisnickiProfilComponent} from "./korisnicki-profil/korisnicki-profil.component";
import {NekretnineVlasnikComponent} from "./nekretnine-vlasnik/nekretnine-vlasnik.component";
import {RezervacijaComponent} from "./rezervacija/rezervacija.component";
import {NavComponent} from "./nav/nav.component";


const routes: Routes = [
  { path: 'pocetna', component: PocetnaComponent},
  { path: 'pomoc', component: PomocComponent},
  { path: 'kategorija', component: KategorijaComponent },
  { path: 'login', component: LogInComponent },
  { path: 'nekretnine/:id', component: NekretnineComponent },
  { path: 'kuce', component: KuceComponent },
  { path: 'stanovi', component: StanoviComponent },
  { path: 'registracija', component: RegistracijaComponent },
  { path: 'detalji-nekretnine/:id', component: DetaljiNekretnineComponent },
  { path: 'nekretnine-vlasnik/:id', component: NekretnineVlasnikComponent },
  { path: 'rezervacija/:id', component: RezervacijaComponent},
  { path: 'add-nekretnina', component: AddNekretninaComponent },
  { path: 'korisnicki-profil', component: KorisnickiProfilComponent },
  { path: 'nav', component: NavComponent }
];

@NgModule({
  imports: [CommonModule,
    RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
