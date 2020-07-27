import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { QuestionElementComponent } from "./question-element/question-element.component"
import { HomeComponent } from "./home-page/home.component"
import { HomeRoutingModule } from "./home-routing.module";
import { SharedModule } from "../shared/shared.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    HomeRoutingModule
  ],
  declarations: [
    QuestionElementComponent,
    HomeComponent
  ]
})
export class HomeModule {
}
