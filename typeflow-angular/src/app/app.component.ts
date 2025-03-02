import { Component } from '@angular/core';
import { UserLoginComponent } from "./auth/user-login/user-login.component";

@Component({
  selector: 'app-root',
  imports: [
    UserLoginComponent
  ],
  providers: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
}
