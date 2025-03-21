import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { UserLogin } from '../models/auth-models';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../services/user/user.service';
import { Router, RouterLink } from '@angular/router';
import { requiredDigit } from '../../../shared/validators/required-digit';
import { requiredLowercase } from '../../../shared/validators/required-lowercase';
import { requiredUppercase } from '../../../shared/validators/required-upperCase';
import { requiredNonAlphanumeric } from '../../../shared/validators/required-non-alphanumeric';
import { CommonModule } from '@angular/common';
import { take } from 'rxjs';

@Component({
	selector: 'tf-user-login',
	imports: [
		FormsModule,
		ReactiveFormsModule,
		MatIconModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
		RouterLink,
		CommonModule
	],
	templateUrl: './user-login.component.html',
	styleUrls: ['./user-login.component.scss', '../shared-styles/user-auth-forms.scss']
})
export class UserLoginComponent {
	public loginForm!: FormGroup;
	loginFailedError: boolean = false;

	constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {
		this.createForm();

	}

	createForm() {
		this.loginForm = this.fb.group({
			userName: ['', Validators.required],
			password: ['', [Validators.required,
				Validators.minLength(12),
				requiredDigit(),
				requiredLowercase(),
				requiredUppercase(),
				requiredNonAlphanumeric()]]
		});
	}

	login() {
		const loginFormValue = this.loginForm.getRawValue();

		if (this.loginForm.valid) {
			const loginRequest: UserLogin = {
				userName: loginFormValue.userName,
				password: loginFormValue.password
			};

			this.userService.login(loginRequest)
			.pipe(
				take(1)
			).subscribe({
				next:() => {
					this.loginFailedError = false;
					this.router.navigate(['/home']);
				},
				error: (error) => {
					this.loginFailedError = true;
					console.log(error);
				}
			});
		}

	}

	public hasError(controlName: string, errorName: string): boolean {
		if(!controlName || !errorName) return false;
	
		return this.loginForm.get(controlName)?.hasError(errorName) ?? false;
	  }
	
}
