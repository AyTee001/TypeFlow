import { TestBed } from '@angular/core/testing';

import { TypingChallengeService } from './typing-challenge.service';

describe('TypingChallengeService', () => {
  let service: TypingChallengeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TypingChallengeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
