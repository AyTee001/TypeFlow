import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStatsChartsComponent } from './user-stats-charts.component';

describe('UserStatsChartsComponent', () => {
  let component: UserStatsChartsComponent;
  let fixture: ComponentFixture<UserStatsChartsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserStatsChartsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserStatsChartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
