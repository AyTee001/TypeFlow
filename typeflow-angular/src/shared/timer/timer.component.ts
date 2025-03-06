import { Component } from '@angular/core';

@Component({
	selector: 'tf-timer',
	imports: [],
	templateUrl: './timer.component.html',
	styleUrl: './timer.component.scss'
})
export class TimerComponent {
	private timeElapsed = 0;
	private interval: any;

	get formattedTime(): string {
		const minutes = Math.floor(this.timeElapsed / 60);
		const seconds = this.timeElapsed % 60;
		return `${this.pad(minutes)}:${this.pad(seconds)}`;
	}

	private pad(value: number): string {
		return value < 10 ? '0' + value : value.toString();
	}

	startTimer() {
		if (!this.interval) {
		  this.interval = setInterval(() => {
			this.timeElapsed++;
		  }, 1000);
		}
	  }
	
	  stopTimer() {
		clearInterval(this.interval);
		this.interval = null;
	  }
	
	  resetTimer() {
		this.stopTimer();
		this.timeElapsed = 0;
	  }
	
	  ngOnDestroy() {
		clearInterval(this.interval);
	  }
}
