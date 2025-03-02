import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { UserLogin } from '../models/auth-models';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../services/user/user.service';
import { RouterLink } from '@angular/router';

@Component({
	selector: 'tf-user-login',
	imports: [
		FormsModule,
		ReactiveFormsModule,
		MatIconModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
		RouterLink
	],
	templateUrl: './user-login.component.html',
	styleUrls: ['./user-login.component.scss', '../shared-styles/user-auth-forms.scss']
})
export class UserLoginComponent {
	public loginForm!: FormGroup;

	constructor(private fb: FormBuilder, private userService: UserService) {
		this.createForm();

	}

	createForm() {
		this.loginForm = this.fb.group({
			userName: ['', Validators.required],
			password: ['', Validators.required]
		});
	}

	login() {
		const loginFormValue = this.loginForm.getRawValue();

		if (this.loginForm.valid) {
			const loginRequest: UserLogin = {
				userName: loginFormValue.userName,
				password: loginFormValue.password
			};

			this.userService.login(loginRequest);
		}

	}
}
