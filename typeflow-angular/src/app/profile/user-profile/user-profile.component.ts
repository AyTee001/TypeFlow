import { Component, OnInit } from '@angular/core';
import { UserService } from '../../auth/services/user/user.service';
import { FullUserData, UserData } from '../../auth/models/user.models';
import { DatePipe } from '@angular/common';
import { take } from 'rxjs';
import { HeaderComponent } from '../../../shared/header/header.component';
import { UserStatsChartsComponent } from '../user-stats-charts/user-stats-charts.component';

@Component({
	selector: 'tf-user-profile',
	imports: [DatePipe, HeaderComponent, UserStatsChartsComponent],
	templateUrl: './user-profile.component.html',
	styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
	public user: FullUserData | null | undefined;

	constructor(private userService: UserService) {

	}

	ngOnInit(): void {
		this.userService.getFullUserData()
		.pipe(
			take(1)
		).subscribe({
			next: (user) => {
				this.user = user;
			},
			error: (err) => {
				console.log(err);
			}
		});
	}

}
