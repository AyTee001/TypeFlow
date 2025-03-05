import { Component, OnInit } from '@angular/core';
import { UserService } from '../../auth/services/user/user.service';
import { FullUserData, UserData } from '../../auth/models/user.models';

@Component({
	selector: 'tf-user-profile',
	imports: [],
	templateUrl: './user-profile.component.html',
	styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
	public user: FullUserData | null | undefined;

	constructor(private userService: UserService) {

	}
	ngOnInit(): void {
	}

}
