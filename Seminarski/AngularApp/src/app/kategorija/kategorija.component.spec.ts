import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KategorijaComponent } from './kategorija.component';

describe('KategorijaComponent', () => {
  let component: KategorijaComponent;
  let fixture: ComponentFixture<KategorijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KategorijaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KategorijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
