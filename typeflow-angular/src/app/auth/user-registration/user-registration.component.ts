import { Component } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { UserRegistration } from '../models/auth-models';
import { FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { valueTypeEqualityValidator } from '../validators/value-type-equality.validator';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'tf-user-registration',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss', '../shared-styles/user-auth-forms.scss']

})
export class UserRegistrationComponent {
  public registrationForm!: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.createForm();

  }

  createForm() {
    this.registrationForm = this.fb.group({
      userName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      repeatedPassword: ['', [valueTypeEqualityValidator('password'),Validators.required]]
    });
  }

  login() {
    const registrationFormValue = this.registrationForm.getRawValue();

    if (this.registrationForm.valid) {
      const registrationRequest: UserRegistration = {
        userName: registrationFormValue.userName,
        email: registrationFormValue.email,
        password: registrationFormValue.password
      };

      this.userService.register(registrationRequest);
    }

  }
}
