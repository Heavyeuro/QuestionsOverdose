import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './_helpers';
import { RoleEnum } from './_models';

const routes: Routes = [
  {
    path: '', 
    loadChildren: () => import("./home/home.module").then(m => m.HomeModule)
  },
  {
    path: 'auth',
    loadChildren: () => import("./auth/auth.module").then(m => m.AuthModule)
  },
  {
    path: 'profile',
    canActivate: [AuthGuard],
    data: { roles: [RoleEnum.Admin] },
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'question',
    loadChildren: () => import("./question/question.module").then(m => m.QuestionModule)
  },
  { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
