import { TestBed } from '@angular/core/testing';

import { Kmmc_AuthService } from './kmmc_auth.service';

describe('AuthService', () => {
  let service: Kmmc_AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Kmmc_AuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
