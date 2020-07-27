import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { EmailFormComponent } from "./email-form/email-form.component";
import { PassFormComponent } from "./password-form/password-form.component";
import { NameFormComponent } from "./name-form/name-form.component";
import { SubscribeTagComponent } from "./subscribe-tag/subscribe-tag.component";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ProfileRoutingModule
  ],
  declarations: [
    ProfileComponent,
    EmailFormComponent,
    PassFormComponent,
    NameFormComponent,
    SubscribeTagComponent
  ]
})
export class ProfileModule {
}
