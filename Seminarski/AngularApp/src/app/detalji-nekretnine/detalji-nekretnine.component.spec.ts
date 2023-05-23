import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetaljiNekretnineComponent } from './detalji-nekretnine.component';

describe('DetaljiNekretnineComponent', () => {
  let component: DetaljiNekretnineComponent;
  let fixture: ComponentFixture<DetaljiNekretnineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetaljiNekretnineComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetaljiNekretnineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
