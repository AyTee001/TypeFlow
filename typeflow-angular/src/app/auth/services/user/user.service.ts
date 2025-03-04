import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserSessionService } from '../user-session/user-session.service';
import { UserLogin, UserRegistration } from '../../models/auth-models';
import { switchMap, take, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserData } from '../../models/user.models';

@Injectable({
	providedIn: 'root'
})
export class UserService {
	private apiUrl: string = 'https://localhost:7244/';
	private userDataEndpoint: string = `${this.apiUrl}user/getUser`;

	public get user(): UserData | null {
		if (!this._user) {
			this._user = this.sessionService.getUserData();
		}
		return this._user;
	}
	private _user?: UserData | null;

	constructor(private authService: AuthService, private sessionService: UserSessionService, private httpClient: HttpClient) { }

	public login(loginData: UserLogin) {
		this.authService.login(loginData)
		.pipe(
			take(1),
			tap((tokens) => {
				this.sessionService.setSession(tokens);
			}),
			switchMap(() => {
				return this.httpClient.get<UserData>(this.userDataEndpoint).pipe(take(1));
			})
		).subscribe({
			next: (userData) => {
				this._user = userData;
				this.sessionService.setUserData(userData);
			},
			error: (error) => {
				console.error(error);
			}
		});
	}

	public logout() {
		const tokens = this.sessionService.getTokens();
		if(!tokens) return;

		this.authService.logout(tokens)
		.pipe(
			take(1),
			tap(() => {
				this.sessionService.revokeSession();
			})
		).subscribe({
			next: () => {
				this._user = null;
			},
			error: (error) => {
				console.error(error);
			}
		});
	}

	public register(registrationData: UserRegistration): void {
		this.authService.register(registrationData)
		.pipe(
			take(1),
			tap((tokens) => {
				this.sessionService.setSession(tokens);
			}),
			switchMap(() => {
				return this.httpClient.get<UserData>(this.userDataEndpoint).pipe(take(1));
			})
		).subscribe({
			next: (userData) => {
				this._user = userData;
				this.sessionService.setUserData(userData);
			},
			error: (error) => {
				console.error(error);
			}
		});
	}

	public isAuthenticated(): boolean {
		return !!this._user;
	}

	public getUser(): UserData | undefined | null{
		return this._user;
	}

}
