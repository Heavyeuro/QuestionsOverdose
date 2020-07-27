import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '../../_services';
import { User } from '../../_models';
import { UserService } from "../user.service"
import { Profile } from '../models'

@Component({
  selector: 'email-form',
  templateUrl: 'email-form.component.html'
})
export class EmailFormComponent implements OnInit {
  currentUser: User;
  modelUser: Profile = new Profile;

  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService,
  ) {}

  ngOnInit() {
    this.currentUser = this.authenticationService.currentUserValue;
    this.userService.getProfile(this.currentUser).subscribe((data: Profile) => this.modelUser = data);
  }

  changeEmail() {
    //TODO: add some checks
    let status;
    this.userService.profilePostRequest("/profile/email", this.modelUser).subscribe(x => status = x);
    //TODO: alert service
    window.location.reload();
  }
}
