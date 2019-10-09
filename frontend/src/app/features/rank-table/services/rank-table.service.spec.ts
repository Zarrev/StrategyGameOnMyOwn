import { TestBed } from '@angular/core/testing';

import { RankTableService } from './rank-table.service';

describe('RankTableService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RankTableService = TestBed.get(RankTableService);
    expect(service).toBeTruthy();
  });
});
