import { TestBed } from '@angular/core/testing';

import { Kmmc_UserService } from './kmmc_user.service';

describe('UserService', () => {
  let service: Kmmc_UserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Kmmc_UserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
