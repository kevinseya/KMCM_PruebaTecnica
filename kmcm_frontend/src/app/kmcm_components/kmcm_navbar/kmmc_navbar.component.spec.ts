import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Kmmc_NavbarComponent } from './kmmc_navbar.component';

describe('NavbarComponent', () => {
  let component: Kmmc_NavbarComponent;
  let fixture: ComponentFixture<Kmmc_NavbarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Kmmc_NavbarComponent]
    });
    fixture = TestBed.createComponent(Kmmc_NavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
