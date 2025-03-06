import { Component } from '@angular/core';
import { HeaderComponent } from '../../../shared/header/header.component';
import { TimerComponent } from '../../../shared/timer/timer.component';
import { MatIconModule } from '@angular/material/icon';
@Component({
	selector: 'tf-home-page',
	imports: [HeaderComponent, TimerComponent, MatIconModule],
	templateUrl: './home-page.component.html',
	styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
