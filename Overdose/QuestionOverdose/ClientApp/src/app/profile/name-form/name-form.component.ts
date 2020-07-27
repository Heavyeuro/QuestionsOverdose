import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '../../_services';
import { User } from '../../_models';
import { UserService } from "../user.service"
import { Profile } from '../models'

@Component({
  selector: 'name-form',
  templateUrl: 'name-form.component.html'
})
export class NameFormComponent implements OnInit {
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

  changeName() {
    //TODO: some check
    let status;
    this.userService.profilePostRequest("/profile/name", this.modelUser).subscribe(x => status = x);

    //TODO: alert service
    window.location.reload();
  }
}
