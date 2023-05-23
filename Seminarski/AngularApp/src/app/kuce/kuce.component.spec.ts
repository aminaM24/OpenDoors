import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KuceComponent } from './kuce.component';

describe('KuceComponent', () => {
  let component: KuceComponent;
  let fixture: ComponentFixture<KuceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KuceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KuceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
