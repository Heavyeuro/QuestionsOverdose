import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from "./home-page/home.component"


const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'questions',
    component: HomeComponent
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
