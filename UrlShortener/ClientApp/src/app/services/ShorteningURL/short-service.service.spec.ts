import { TestBed } from '@angular/core/testing';

import { ShortServiceService } from './short-service.service';

describe('ShortServiceService', () => {
  let service: ShortServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShortServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
