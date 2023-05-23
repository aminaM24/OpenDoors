import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NekretnineVlasnikComponent } from './nekretnine-vlasnik.component';

describe('NekretnineVlasnikComponent', () => {
  let component: NekretnineVlasnikComponent;
  let fixture: ComponentFixture<NekretnineVlasnikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NekretnineVlasnikComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NekretnineVlasnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
