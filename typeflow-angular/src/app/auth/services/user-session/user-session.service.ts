import { Injectable } from '@angular/core';
import { Tokens } from '../../models/auth-models';
import dayjs from 'dayjs';

@Injectable({
	providedIn: 'root'
})
export class UserSessionService {
	private refreshTokenName: string = 'TypeFlowRefreshToken';
	private accessTokenName: string = 'TypeFlowAccessToken';
	private refreshTokenExpirationName: string = 'TypeFlowRefreshTokenExpiration';
	private accessTokenExpirationName: string = 'TypeFlowAccessTokenExpiration';

	constructor() { }

	setSession(tokens: Tokens) {
		if(!tokens) return;

		localStorage.setItem(this.accessTokenName, tokens.accessToken);
		localStorage.setItem(this.refreshTokenName, tokens.refreshToken);
		localStorage.setItem(this.accessTokenExpirationName, tokens.accessTokenExpiration.toString());
		localStorage.setItem(this.refreshTokenExpirationName, tokens.refreshTokenExpiration.toString());
	}

	revokeSession() {
		localStorage.removeItem(this.accessTokenName);
		localStorage.removeItem(this.refreshTokenName);
		localStorage.removeItem(this.accessTokenExpirationName);
		localStorage.removeItem(this.refreshTokenExpirationName);
	}

	getTokens(): Tokens {
		const tokens: Tokens = {
			accessToken: this.getAccessToken() || '',
			refreshToken: this.getRefreshToken() || '',
			accessTokenExpiration: this.getAccessTokenExpiration()?.toDate() || dayjs().toDate(),
			refreshTokenExpiration: this.getRefreshTokenExpiration()?.toDate() || dayjs().toDate()
		}
		return tokens;
	}

	getAccessToken(): string | null {
		return localStorage.getItem(this.accessTokenName);
	}

	getRefreshToken(): string | null {
		return localStorage.getItem(this.refreshTokenName);
	}

	getAccessTokenExpiration(): dayjs.Dayjs | null {
		return this.getExpirationDate(this.accessTokenExpirationName);
	}

	getRefreshTokenExpiration(): dayjs.Dayjs | null {
		return this.getExpirationDate(this.refreshTokenExpirationName);
	}

	private getExpirationDate(tokenName: string): dayjs.Dayjs | null {
		if(!tokenName) return null;

		let expirationString = localStorage.getItem(tokenName);
		if(!expirationString) return null;

		let date = dayjs(expirationString);
		if(!date.isValid()) return null;

		return date;
	}
}
