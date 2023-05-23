import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StanoviComponent } from './stanovi.component';

describe('StanoviComponent', () => {
  let component: StanoviComponent;
  let fixture: ComponentFixture<StanoviComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StanoviComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StanoviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
