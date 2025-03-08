import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TypingSessionData, TypingSessionResult } from '../../models/typing-session.models';

@Injectable({
	providedIn: 'root'
})
export class TypingSessionService {
	private apiUrl: string = 'https://localhost:7244/';
	private recordSessionEndpoint: string = `${this.apiUrl}typingSession/recordSession`;

	constructor(private httpClient: HttpClient) { }


	public recordTypingSession(typingSessionData: TypingSessionData) {
		return this.httpClient.post<TypingSessionResult>(this.recordSessionEndpoint, typingSessionData);
	}

}
