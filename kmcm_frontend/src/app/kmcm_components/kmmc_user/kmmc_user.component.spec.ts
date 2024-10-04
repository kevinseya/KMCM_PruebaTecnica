import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Kmmc_UserComponent } from './kmmc_user.component';

describe('KmmcUserComponent', () => {
  let component: Kmmc_UserComponent;
  let fixture: ComponentFixture<Kmmc_UserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Kmmc_UserComponent]
    });
    fixture = TestBed.createComponent(Kmmc_UserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
