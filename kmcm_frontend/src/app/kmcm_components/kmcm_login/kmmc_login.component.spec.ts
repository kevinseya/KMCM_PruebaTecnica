import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Kmmc_LoginComponent } from './kmmc_login.component';

describe('LoginComponent', () => {
  let component: Kmmc_LoginComponent;
  let fixture: ComponentFixture<Kmmc_LoginComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Kmmc_LoginComponent]
    });
    fixture = TestBed.createComponent(Kmmc_LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
