import { TestBed } from '@angular/core/testing';

import { InternalErrorService } from './internal-error.service';

describe('InternalErrorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InternalErrorService = TestBed.get(InternalErrorService);
    expect(service).toBeTruthy();
  });
});
