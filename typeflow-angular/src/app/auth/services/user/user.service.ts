import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserSessionService } from '../user-session/user-session.service';
import { UserLogin, UserRegistration } from '../../models/auth-models';
import { Observable, of, switchMap, take, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserData } from '../../models/user.models';

@Injectable({
	providedIn: 'root'
})
export class UserService {
	private apiUrl: string = 'https://localhost:7244/';
	private userDataEndpoint: string = `${this.apiUrl}user/getUser`;

	private _user?: UserData | null;

	constructor(private authService: AuthService, private sessionService: UserSessionService, private httpClient: HttpClient) { }

	public login(loginData: UserLogin) {
		return this.authService.login(loginData)
		.pipe(
			take(1),
			tap((tokens) => {
				this.sessionService.setSession(tokens);
			}),
			switchMap(() => {
				return this.httpClient.get<UserData>(this.userDataEndpoint).pipe(take(1));
			}),
			tap((userData) => {
				this._user = userData;
			})
		);
	}

	public logout() {
		const tokens = this.sessionService.getTokens();
		if(!tokens) return;

		this.authService.logout(tokens)
		.pipe(
			take(1),
			tap(() => {
				this._user = null;
			})
		).subscribe();
	}

	public register(registrationData: UserRegistration) {
		return this.authService.register(registrationData)
		.pipe(
			take(1),
			tap((tokens) => {
				this.sessionService.setSession(tokens);
			}),
			switchMap(() => {
				return this.httpClient.get<UserData>(this.userDataEndpoint).pipe(take(1));
			}),
			tap((userData) => {
				this._user = userData;
			})
		);
	}

	public isAuthenticated(): boolean {
		return !!this._user;
	}


	public get user(): UserData | null | undefined {
		return this._user;
	}

	public init(): Observable<UserData | null> {
		return this.httpClient.get<UserData>(this.userDataEndpoint)
		.pipe(
			take(1),
			tap((userData) => {
				this._user = userData;
				this.sessionService.setUserData(userData);
			})
		)
	}

}
