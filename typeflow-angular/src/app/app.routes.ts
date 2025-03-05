import { Routes } from '@angular/router';
import { UserLoginComponent } from './auth/user-login/user-login.component';
import { UserRegistrationComponent } from './auth/user-registration/user-registration.component';
import { anonymousOnlyGuard, authenticatedOnlyGuard } from './auth/guards/auth.guard';
import { HomePageComponent } from './home/home-page/home-page.component';
import { UserProfileComponent } from './profile/user-profile/user-profile.component';

export const routes: Routes = [
    { path: 'login', pathMatch: 'full', component: UserLoginComponent, canActivate: [anonymousOnlyGuard] },
    { path: 'registration', pathMatch: 'full', component: UserRegistrationComponent, canActivate: [anonymousOnlyGuard] },
    { path: 'home', pathMatch: 'full', component: HomePageComponent },
    { path: 'profile', pathMatch: 'full', component: UserProfileComponent, canActivate: [authenticatedOnlyGuard] },
    { path: '', pathMatch: 'full', redirectTo: 'login' }
];
