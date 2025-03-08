import { TestBed } from '@angular/core/testing';

import { TypingSessionService } from './typing-session.service';

describe('TypingSessionService', () => {
  let service: TypingSessionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TypingSessionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
