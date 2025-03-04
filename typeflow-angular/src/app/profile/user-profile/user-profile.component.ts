import { Component, OnInit } from '@angular/core';
import { UserService } from '../../auth/services/user/user.service';
import { UserData } from '../../auth/models/user.models';

@Component({
  selector: 'tf-user-profile',
  imports: [],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
  public user?: UserData | null;

  constructor(private userService: UserService) {

  }
  ngOnInit(): void {
    this.userService.getUser().subscribe((user: UserData | null) => {
      this.user = user;
    });
  }

}
