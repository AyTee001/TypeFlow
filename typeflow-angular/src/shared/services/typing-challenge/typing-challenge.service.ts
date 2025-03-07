import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TypingChallengeData } from '../../models/typing-challenge.models';

@Injectable({
	providedIn: 'root'
})
export class TypingChallengeService {
	private apiUrl: string = 'https://localhost:7244/';
	private randomChallengeEndpoint: string = `${this.apiUrl}typingChallenge/random`;


	constructor(private httpClient: HttpClient) { }

	public getRandomChallenge(){
		return this.httpClient.get<TypingChallengeData>(this.randomChallengeEndpoint);
	}
}
