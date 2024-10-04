import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Kmmc_MenuComponent } from './kmmc_-menu.component';

describe('KmmcMenuComponent', () => {
  let component: Kmmc_MenuComponent;
  let fixture: ComponentFixture<Kmmc_MenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Kmmc_MenuComponent]
    });
    fixture = TestBed.createComponent(Kmmc_MenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
