import { TestBed } from '@angular/core/testing';

import { DataHashService } from './data-hash.service';

describe('DataHashService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DataHashService = TestBed.get(DataHashService);
    expect(service).toBeTruthy();
  });
});
