import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { TypingSessionService } from '../../../shared/services/typing-session/typing-session.service';
import Chart from 'chart.js/auto';

@Component({
	selector: 'tf-user-stats-charts',
	imports: [],
	templateUrl: './user-stats-charts.component.html',
	styleUrl: './user-stats-charts.component.scss'
})
export class UserStatsChartsComponent implements AfterViewInit {
	@ViewChild('chartContainer') chartContainer!: ElementRef<HTMLDivElement>;
	
	@Input() chartId: string = 'canvas';

	public chart: any = [];

	constructor(private typingSessionService: TypingSessionService) {

	}

	ngAfterViewInit(): void {
		const canvas = document.createElement('canvas');
		canvas.id = this.chartId;
	
		this.chartContainer.nativeElement.appendChild(canvas);

		this.chart = new Chart(this.chartId, {
			type: 'line',

			data: {
				labels: ['2022-05-10', '2022-05-11', '2022-05-12', '2022-05-13',
					'2022-05-14', '2022-05-15', '2022-05-16', '2022-05-17',],
				datasets: [
					{
						label: "WPM",
						data: ['467', '576', '572', '79', '92',
							'574', '573', '576'],
						backgroundColor: '#a6c8ff',
						borderColor: 'rgba(210, 228, 255, 0.4)'
					},
				]
			},

			options: {
				aspectRatio: 2.5,
				responsive: true,
				scales: {
					x: {
						title: {
							display: true,
							text: 'Days',
							color: '#a6c8ff'
						}, ticks: {
							color: '#a6c8ff'
						},
						grid: {
							color: 'rgba(210, 228, 255, 0.4)'
						}
					},
					y: {
						title: {
							display: true,
							text: 'WPM rate',
							color: '#a6c8ff'
						}, ticks: {
							color: '#a6c8ff'
						},
						grid: {
							color: 'rgba(210, 228, 255, 0.4)'
						}
					},
				},
			}

		});
	}

}
