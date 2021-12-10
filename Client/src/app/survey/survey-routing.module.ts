import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionComponent } from './question/question.component';
// import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
    // { path: 'me', component:  ProfileComponent},
    { path:'question', component: QuestionComponent, },

  ];
  
  @NgModule({
    imports: [
      RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
  })
  
  export class SurveyRoutingModule { } 