import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Kmmc_PersonComponent } from './kmmc_person.component';

describe('PersonComponent', () => {
  let component: Kmmc_PersonComponent;
  let fixture: ComponentFixture<Kmmc_PersonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Kmmc_PersonComponent]
    });
    fixture = TestBed.createComponent(Kmmc_PersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
