import { TestBed } from '@angular/core/testing';

import { Kmmc_PersonService } from './kmmc_person.service';

describe('PersonService', () => {
  let service: Kmmc_PersonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Kmmc_PersonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
