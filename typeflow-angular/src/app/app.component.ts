import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserService } from './auth/services/user/user.service';
import { UserData } from './auth/models/user.models';

@Component({
	selector: 'app-root',
	imports: [
		RouterOutlet
	],
	providers: [],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss'
})
export class AppComponent {
	constructor(public userService: UserService){ 
	}
}
