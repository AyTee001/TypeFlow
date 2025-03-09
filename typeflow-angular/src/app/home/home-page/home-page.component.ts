import { Component, OnInit, ViewChild } from '@angular/core';
import { HeaderComponent } from '../../../shared/header/header.component';
import { TimerComponent } from '../../../shared/timer/timer.component';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { TypingChallengeService } from '../../../shared/services/typing-challenge/typing-challenge.service';
import { TypingChallengeData } from '../../../shared/models/typing-challenge.models';
import { take, tap } from 'rxjs';
import { TypingSessionService } from '../../../shared/services/typing-session/typing-session.service';
import { TypingSessionData, TypingSessionDisplayResult } from '../../../shared/models/typing-session.models';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { UserService } from '../../auth/services/user/user.service';
import { MatDialog } from '@angular/material/dialog';
import { TypingSessionResultDialogComponent } from '../dialogs/typing-session-result-dialog/typing-session-result-dialog.component';

@Component({
	selector: 'tf-home-page',
	imports: [MatProgressSpinnerModule, HeaderComponent, TimerComponent, MatIconModule, MatInputModule, MatFormFieldModule, FormsModule, MatButtonModule],
	templateUrl: './home-page.component.html',
	styleUrl: './home-page.component.scss'
})
export class HomePageComponent implements OnInit {
	@ViewChild(TimerComponent) timerComponent!: TimerComponent; 

	public get challenge(){
		return this._challenge;
	}
	private _challenge: TypingChallengeData | undefined;
	public textAreaDiabled: boolean = false;

	public get challengeResult(){
		return this._challengeResult;
	}
	private _challengeResult: TypingSessionDisplayResult | undefined;
	public savingInProgress: boolean = false;

	public completionMessage = "";
	public userText: string = "";

	private _sessionInProgress: boolean = false;
	private _currentErrorCount: number = 0;

	constructor(private typingChallengeService: TypingChallengeService,
		private typingSessionService: TypingSessionService,
		private userSercice: UserService,
		private dialog: MatDialog){

	}

	ngOnInit(){
		this.loadNextChallenge();
	}


	public loadNextChallenge(){
		this.resetSession();
		this.typingChallengeService.getRandomChallenge()
			.pipe(take(1))
			.subscribe({
				next: (res) => {
					this._challenge = res;
				},
				error: (error) => {
					console.log(error)
				}
			})
	}

	public onKeyDown(event: Event){
		if(!this._sessionInProgress){
			this.startSession();
		}

		const forbiddenKeys = ['ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown', 'Delete', 'Home', 'End'];

		if(forbiddenKeys.includes((event as any).key)){
			event.preventDefault();
			return;
		}

		if ((event as any).key !== 'Backspace' && !(event as any).key.match(/^[a-zA-Z0-9 .,!?()'"[\]{}&%$#@;:<>_^+=\-*|\\/`~\r\n\t]*$/)) {
			event.preventDefault();
		}

		this.handleKeyDown((event as any).key);

		if(this.shouldFinalizeSession()){
			this.stopSession();
		}
	}

	private startSession(){
		this.timerComponent?.resetTimer();
		this.timerComponent?.startTimer();
		this._sessionInProgress = true;
		this._currentErrorCount = 0;
		this.completionMessage = "";
	}

	private stopSession(){
		this.timerComponent?.stopTimer();
		this._sessionInProgress = false;
		this.textAreaDiabled = true;
		
		if(this.userSercice.isAuthenticated()){
			this.saveSession();
		}
		else{
			this.getUnsavedResult();
		}
	}

	private getUnsavedResult(){
		if(!this._challenge) return;

		const finishedInSeconds = this.timerComponent.getCurrentTimeSeconds();
		const characterCount = this._challenge.text.length;
		const errorCount = this._currentErrorCount;

		const minutes = finishedInSeconds / 60;

    	const safeMinutes = minutes > 0 ? minutes : 1 / 60;

    	const accuracy = ((characterCount - errorCount) / characterCount) * 100;
    	const wpm = Math.floor(characterCount / (5 * safeMinutes));
    	const cpm = Math.floor(characterCount / safeMinutes);

		const result: TypingSessionDisplayResult = {
			wordsPerMinute: wpm,
			charactersPerMinute: cpm,
			accuracy: accuracy,
			finishedInSeconds: finishedInSeconds,
			errors: errorCount,
			charactersCount: characterCount
		}

		this._challengeResult = result;

		this.dialog.open(TypingSessionResultDialogComponent, {
			data: this._challengeResult
		});
	}

	private saveSession(){
		if(!this._challenge) return;

		const sessionPayload: TypingSessionData = {
			challengeId: this._challenge.id,
			characterCount: this._challenge.text.length,
			errorCount: this._currentErrorCount,
			finishedInSeconds: this.timerComponent.getCurrentTimeSeconds()
		};

		this.typingSessionService.recordTypingSession(sessionPayload)
		.pipe(
			tap(() => this.savingInProgress = true),
			take(1)
		).subscribe({
			next: (res) => {
				this._challengeResult = res as TypingSessionDisplayResult;
				this.savingInProgress = false;
				this.dialog.open(TypingSessionResultDialogComponent, {
					data: this._challengeResult
				})		
			},
			error: (error) => {
				this.savingInProgress = false;
				console.log(error);
			}
		})
	}

	private resetSession(){
		this.userText = "";
		this.timerComponent?.resetTimer();
		this._sessionInProgress = false;
		this._currentErrorCount = 0;
		this.completionMessage = "";
		this.textAreaDiabled = false;
	}

	private handleKeyDown(key: string) {
		if (!key
			|| !this.userText
			|| !this._challenge?.text
			|| this.userText.length > this._challenge.text.length) {
			return;
		}

		let errors = 0;

		for (let i = 0; i < this.userText.length; i++) {
			if (this.userText[i] !== this._challenge.text[i]) {
				errors++;
			}
		}

		this._currentErrorCount = errors;
	}

	private shouldFinalizeSession(): boolean{
		if(!this.userText || !this._challenge?.text) return false;

		return this.userText.length >= this._challenge.text.length;
	}

	public onMouseDown(event: MouseEvent) {
		event.preventDefault();
	}
	
	public onMouseUp(event: MouseEvent) {
		event.preventDefault();
		this.keepCursorAtEnd();
	}

	public onFocus(event: Event){
		event.preventDefault();
		setTimeout(() => {
			this.keepCursorAtEnd();
		}, 0);
	}

	public onPaste(event: Event){
		event.preventDefault();
	}

	public onCopy(event: Event){
		event.preventDefault();
	}

	private keepCursorAtEnd() {
		const textarea = document.querySelector('#userInput') as any;
		if(!textarea) return;

		textarea.focus();
		textarea.setSelectionRange(textarea.value.length, textarea.value.length);
	}
}
