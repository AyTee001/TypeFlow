import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { UserService } from '../services/user/user.service';

export const anonymousOnlyGuard: CanActivateFn = (route, state) => {
	const userService = inject(UserService);
	const router = inject(Router);

	if (!userService.isAuthenticated()) {
		return true;
	}

	router.navigate(['/home']);
	return false;
};

export const authenticatedOnlyGuard: CanActivateFn = (route, state) => {
	const userService = inject(UserService);
	const router = inject(Router);

	if (!userService.isAuthenticated()) {
		router.navigate(['/login']);
		return false;
	}

	return true;
};
