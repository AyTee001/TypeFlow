import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TypingSessionResultDialogComponent } from './typing-session-result-dialog.component';

describe('TypingSessionResultDialogComponent', () => {
  let component: TypingSessionResultDialogComponent;
  let fixture: ComponentFixture<TypingSessionResultDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TypingSessionResultDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TypingSessionResultDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
