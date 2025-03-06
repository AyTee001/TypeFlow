import { Component } from '@angular/core';
import { HeaderComponent } from '../../../shared/header/header.component';
import { TimerComponent } from '../../../shared/timer/timer.component';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'tf-home-page',
	imports: [HeaderComponent, TimerComponent, MatIconModule, MatInputModule, MatFormFieldModule, FormsModule, MatButtonModule],
	templateUrl: './home-page.component.html',
	styleUrl: './home-page.component.scss'
})
export class HomePageComponent {
	public text: string = "So call me when the work looks bleak. I love you, but it's hard to believe. With every day we'll start to see. The rest is metamodernity";
	public userText: string = "";
}
