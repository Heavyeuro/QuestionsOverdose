import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from 'src/app/_services';
import { User, RoleEnum } from 'src/app/_models';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {
  isExpanded = false;
  currentUser: User;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === RoleEnum.Admin;
  }
  get isRedactor() {
    return this.currentUser && this.currentUser.role === RoleEnum.Redactor;
  }

  logout() {
    this.authenticationService.logout();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
