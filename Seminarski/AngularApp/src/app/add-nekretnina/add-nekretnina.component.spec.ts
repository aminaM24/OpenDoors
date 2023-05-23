import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNekretninaComponent } from './add-nekretnina.component';

describe('AddNekretninaComponent', () => {
  let component: AddNekretninaComponent;
  let fixture: ComponentFixture<AddNekretninaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNekretninaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddNekretninaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
