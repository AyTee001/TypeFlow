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
import { take } from 'rxjs';

@Component({
	selector: 'tf-home-page',
	imports: [HeaderComponent, TimerComponent, MatIconModule, MatInputModule, MatFormFieldModule, FormsModule, MatButtonModule],
	templateUrl: './home-page.component.html',
	styleUrl: './home-page.component.scss'
})
export class HomePageComponent implements OnInit {
	@ViewChild(TimerComponent) timerComponent!: TimerComponent; 

	public get challenge(){
		return this._challenge;
	}
	private _challenge: TypingChallengeData | undefined;
	public completionMessage = "";

	public userText: string = "";

	private _sessionInProgress: boolean = false;
	private _currentErrorCount: number = 0;

	constructor(private typingChallengeService: TypingChallengeService){

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
		this.completionMessage = "You made " + this._currentErrorCount + " errors";
	}

	private resetSession(){
		this.userText = "";
		this.timerComponent?.resetTimer();
		this._sessionInProgress = false;
		this._currentErrorCount = 0;
		this.completionMessage = "";
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
