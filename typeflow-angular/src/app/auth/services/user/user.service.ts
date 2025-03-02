import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserSessionService } from '../user-session/user-session.service';
import { UserLogin, UserRegistration } from '../../models/auth-models';

@Injectable({
	providedIn: 'root'
})
export class UserService {

	constructor(private authService: AuthService, private sessionService: UserSessionService) { }

	//should return user data in the future
	public login(loginData: UserLogin) {
		this.authService.login(loginData)
		.subscribe({
			next: (tokens) => {
				this.sessionService.setSession(tokens);
			},
			error: (error) => {
				console.error(error);
			}
		});
	}

	public logout() {
		const tokens = this.sessionService.getTokens();
		if(!tokens) return;

		this.authService.logout(tokens).subscribe({
			next: () => {
				this.sessionService.revokeSession();
			},
			error: (error) => {
				console.error(error);
			}
		});
	}

	//should return user data in the future
	public register(registrationData: UserRegistration) {
		this.authService.register(registrationData)
		.subscribe({
			next: (tokens) => {
				this.sessionService.setSession(tokens);
			},
			error: (error) => {
				console.error(error);
			}
		});
	}

	public isAuthenticated() {
		return true;
	}

	public getUser() {
		return {
			name: 'John Doe',
			email: 'sth@mail.com'
		};
	}

}
