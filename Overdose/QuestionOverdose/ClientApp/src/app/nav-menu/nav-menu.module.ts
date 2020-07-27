import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NavBarComponent } from "./nav-bar/nav-bar.component";
import { QuestionLiveSearchComponent } from "./live-search/live-search.component"


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  declarations: [
    NavBarComponent,
    QuestionLiveSearchComponent
  ],
  exports: [ NavBarComponent ]
})
export class NavMenuModule {
}
