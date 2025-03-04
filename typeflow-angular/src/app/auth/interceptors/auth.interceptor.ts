import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { UserSessionService } from '../services/user-session/user-session.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
	const sessionService = inject(UserSessionService);
	const authService = inject(AuthService);

	const token = sessionService.getAccessToken();

	if (!token) {
		return next(req);
	}

	let cloned = addToken(req, token);

	return next(cloned).pipe(
		catchError(error => {
			if (error.status === 401 && token) {
				handleTokenExpiration(req, next);
			}
			return throwError(() => error);
		})
	);


	function handleTokenExpiration(request: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
		const router = inject(Router);

		const refreshToken = sessionService.getRefreshToken();
		if (!refreshToken) {
			router.navigate(['/login']);
			return throwError(() => new Error('No refresh token available'));
		}

		return authService.refreshToken(refreshToken).pipe(
			switchMap((tokens) => {
				sessionService.setSession(tokens);
				let newAccessToken = sessionService.getAccessToken();
				return next(addToken(request, newAccessToken));
			}),
			catchError((error) => {
				console.error('Error handling expired access token:', error);
				router.navigate(['/login']);
				return throwError(() => error);
			})
		);

	}

	function addToken(request: HttpRequest<any>, token: string | null): HttpRequest<any> {
		const cloned = request.clone({
			setHeaders: {
				Authorization: `Bearer ${token}`
			}
		})

		return cloned;
	}

};
