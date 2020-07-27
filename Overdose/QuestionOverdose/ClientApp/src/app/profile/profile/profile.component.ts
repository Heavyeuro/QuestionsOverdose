import { Component, OnInit } from '@angular/core';

import { AuthenticationService} from '../../_services';
import { User } from '../../_models';
import { UserService } from "../user.service"
import { Profile, DisplayForm } from '../models'

@Component({
  selector: 'profile-component',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  currentUser: User;
  profile: Profile = new Profile;
  displayForm: DisplayForm = DisplayForm.None;

  constructor(
    private authenticationService: AuthenticationService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.currentUser = this.authenticationService.currentUserValue;
    this.userService.getProfile(this.currentUser).subscribe((data: Profile) => this.profile = data);
  }

  displayChangeName() {
    this.displayForm = DisplayForm.ChangeName;
  }

  displayChangeEmail() {
    this.displayForm = DisplayForm.ChangeEmail;
  }

  resendEmail() {
    //string userId, string token
    this.userService.profilePostRequest("verify/email", this.profile);
    //TODO:request to the specific controller
  }

  displayChangePass() {
    this.displayForm = DisplayForm.ChangePass;
  }
}
