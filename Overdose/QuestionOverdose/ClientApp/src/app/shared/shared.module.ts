import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ModalModule } from 'src/app/_modal'
import { HighlightDirective } from "./highlight/highlight.directive"
import { PagingComponent } from "./paging/paging.component"
import { VoteFeatureComponent } from "./vote-feature/vote-feature.component"
import { AdminControllerComponent } from "./admin-controller/admin-controller.component"

@NgModule({
  imports: [
    ModalModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    HighlightDirective,
    PagingComponent,
    VoteFeatureComponent,
    AdminControllerComponent
  ],
  exports: [
    HighlightDirective,
    PagingComponent,
    VoteFeatureComponent,
    AdminControllerComponent
  ]
})
export class SharedModule {
}

