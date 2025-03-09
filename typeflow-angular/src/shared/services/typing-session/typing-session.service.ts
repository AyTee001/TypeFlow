import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TypingSessionChartStatistics, TypingSessionData, TypingSessionResultData } from '../../models/typing-session.models';

@Injectable({
	providedIn: 'root'
})
export class TypingSessionService {
	private apiUrl: string = 'https://localhost:7244/';
	private recordSessionEndpoint: string = `${this.apiUrl}typingSession/recordSession`;
	private chartEndpoint: string = `${this.apiUrl}typingSession/chart`;

	constructor(private httpClient: HttpClient) { }


	public recordTypingSession(typingSessionData: TypingSessionData) {
		return this.httpClient.post<TypingSessionResultData>(this.recordSessionEndpoint, typingSessionData);
	}

	public getTypingSessionChartStatistics(){
		return this.httpClient.get<TypingSessionChartStatistics>(this.chartEndpoint);
	}
}
