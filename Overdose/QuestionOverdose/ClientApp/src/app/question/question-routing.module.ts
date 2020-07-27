import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AddQuestionComponent } from "./add-question/add-question.component"
import { QuestionComponent } from "./question/question.component";
import { AuthGuard } from "../_helpers/auth.guard";
import { RoleEnum } from "../_models/roleEnum";


const routes: Routes = [
  {
    path: '',
    component: QuestionComponent
  },
  {
    path: 'add', component: AddQuestionComponent,
    canActivate: [AuthGuard],
    data: { roles: [RoleEnum.Admin] }
  },
  { path: ':id', component: QuestionComponent }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuestionRoutingModule { }
