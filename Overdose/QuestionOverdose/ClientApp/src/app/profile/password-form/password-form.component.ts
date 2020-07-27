import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '../../_services';
import { User } from '../../_models';
import { UserService } from "../user.service"
import { Profile } from '../models'

@Component({
  selector: 'password-form',
  templateUrl: 'password-form.component.html'
})
export class PassFormComponent implements OnInit {
  confirmPass: string;
  currentUser: User;
  modelUser: Profile = new Profile;

  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {
    this.userService.getProfile(this.currentUser).subscribe((data: Profile) => {
      this.modelUser = data;
      this.confirmPass = this.modelUser.password;
    });
  }

  changePass() {
    if (this.confirmPass !== this.modelUser.password)
      return;
    if (this.currentUser.password === this.modelUser.password)
      return;

    let status;
    this.userService.profilePostRequest("/profile/pass", this.modelUser).subscribe(x => status = x);

    //TODO: alert service
    window.location.reload();
  }
}
