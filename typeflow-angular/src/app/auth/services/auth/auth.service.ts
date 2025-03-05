import { Injectable } from '@angular/core';
import { UserRegistration, UserLogin, Tokens, RefreshTokenRequest } from '../../models/auth-models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class AuthService {
	private apiUrl: string = 'https://localhost:7244/';
	private refreshTokenEndpoint: string = `${this.apiUrl}auth/refresh`;
	private loginEndpoint: string = `${this.apiUrl}auth/login`;
	private registerEndpoint: string = `${this.apiUrl}auth/register`;
	private logoutEndpoint: string = `${this.apiUrl}auth/logout`;

	constructor(private httpClient: HttpClient) { }

	public register(payload: UserRegistration): Observable<Tokens> {
		return this.httpClient.post<Tokens>(this.registerEndpoint, payload);
	}

	public login(payload: UserLogin) : Observable<Tokens> {
		return this.httpClient.post<Tokens>(this.loginEndpoint, payload);
	}

	public logout(tokens: Tokens): Observable<void> {
		return this.httpClient.post<void>(this.logoutEndpoint, tokens);
	}

	public refreshToken(refreshTokenRequest: RefreshTokenRequest): Observable<Tokens> {
		return this.httpClient.post<Tokens>(this.refreshTokenEndpoint, refreshTokenRequest);
	}

}
