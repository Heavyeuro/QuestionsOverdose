import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

import { QuestionComponent } from './question/question.component';
import { AnswerFormComponent } from './answer-form/answer-form.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { CommentFormComponent } from './comment-form/comment-form.component';
import { QuestionRoutingModule } from "./question-routing.module";
import { SharedModule } from "../shared/shared.module";


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    QuestionRoutingModule,
    SharedModule,
    NgMultiSelectDropDownModule
  ],
  declarations: [
    QuestionComponent,
    AnswerFormComponent,
    AddQuestionComponent,
    CommentFormComponent
  ]
})
export class QuestionModule {
}
