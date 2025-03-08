import { Component, Inject } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog'
import { MatListModule } from '@angular/material/list'
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { DecimalPipe } from '@angular/common';
import { TypingSessionDisplayResult } from '../../../../shared/models/typing-session.models';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'tf-typing-session-result-dialog',
	imports: [MatDialogModule, MatListModule, MatProgressBarModule, DecimalPipe, MatButtonModule],
	templateUrl: './typing-session-result-dialog.component.html',
	styleUrl: './typing-session-result-dialog.component.scss'
})
export class TypingSessionResultDialogComponent {

	constructor(public dialogRef: MatDialogRef<TypingSessionResultDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: TypingSessionDisplayResult) {
	}

}
