import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionComponent } from './question/question.component';
import { SurveyComponent } from './survey/survey.component';
// import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
    // { path: 'me', component:  ProfileComponent},
    { path:'question', component: QuestionComponent, },
    { path:'survey', component: SurveyComponent, },

  ];
  
  @NgModule({
    imports: [
      RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
  })
  
  export class SurveyRoutingModule { } 