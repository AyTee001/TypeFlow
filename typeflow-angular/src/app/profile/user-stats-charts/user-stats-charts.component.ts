import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { TypingSessionService } from '../../../shared/services/typing-session/typing-session.service';
import Chart from 'chart.js/auto';
import { take } from 'rxjs';
import { TypingSessionChartStatistics } from '../../../shared/models/typing-session.models';

@Component({
	selector: 'tf-user-stats-charts',
	imports: [],
	templateUrl: './user-stats-charts.component.html',
	styleUrl: './user-stats-charts.component.scss'
})
export class UserStatsChartsComponent {
	@ViewChild('chartContainer') chartContainer!: ElementRef<HTMLDivElement>;
	
	public chart: any = [];

	constructor(private typingSessionService: TypingSessionService) {
		this.typingSessionService.getTypingSessionChartStatistics()
			.pipe(take(1))
			.subscribe({
				next: (data) => {
					this.createChart(data);
				},
				error: (error) => {
					console.log(error);
				}
			})
	}

	createChart(data: TypingSessionChartStatistics): void {
		if(!data || !data.dates) return;

		const canvas = document.createElement('canvas');
		canvas.id = 'userStats';
	
		this.chartContainer.nativeElement.appendChild(canvas);

		this.chart = new Chart('userStats', {
			type: 'line',

			data: {
				labels: data.dates.map(x => x == null ? null : new Date(x).toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' })),
				datasets: [
					{
						label: "WPM",
						data: data.wpmValues ?? [],
						backgroundColor: '#4CAF50',
						borderColor: 'rgba(76, 175, 80, 0.4)'
					},
					{
						label: "Accuracy",
						data: data.accuracyValues ?? [],
						backgroundColor: '#FF9800',
						borderColor: 'rgba(255, 152, 0, 0.4)'
					},
				]
			},

			options: {
				plugins:{
					legend: {
						labels: {
							color: '#a6c8ff'
						}
					}
				},
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
							text: 'WPM /Accuracy (%)',
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
