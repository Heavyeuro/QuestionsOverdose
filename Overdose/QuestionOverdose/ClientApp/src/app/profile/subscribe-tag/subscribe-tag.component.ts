import { Component, OnInit } from '@angular/core';

import { AuthenticationService, TagService } from '../../_services';
import { User, Tag } from '../../_models';
import { UserService } from "../user.service"
import { Profile } from '../models'

@Component({
  selector: 'subscribe-tag',
  templateUrl: 'subscribe-tag.component.html'
})
export class SubscribeTagComponent implements OnInit {
  currentUser: User;
  modelUser: Profile = new Profile;
  tagsToSubscribe: Tag[] = [];

  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private tagService: TagService
  ) {}

  ngOnInit() {
    this.currentUser = this.authenticationService.currentUserValue;
    this.userService.getProfile(this.currentUser).subscribe((data: Profile) => {
      this.modelUser = data;
      this.tagService.getTags().subscribe(x => {
        const allTags = x;
        this.tagsToSubscribe = allTags.filter(z => this.modelUser.subscribedTags.forEach(y => y !== z.tagName));
      });
    });
  }

  subscribeOnTag(tagId: number) {
    //TODO: some check

    //TODO: alert service
    window.location.reload();
  }
}
