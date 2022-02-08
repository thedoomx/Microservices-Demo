import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionComponent } from './question/question.component';
import { CreateSurveyComponent } from './survey/create/create-survey.component';
import { ListSurveyComponent } from './survey/list/list-survey.component';
import { SurveyComponent } from './survey/survey.component';

const routes: Routes = [
    { path:'question', component: QuestionComponent, },
    { path:'create-survey', component: CreateSurveyComponent, },
    { path:'list-survey', component: ListSurveyComponent, },
    { path:'', component: SurveyComponent, },

  ];
  
  @NgModule({
    imports: [
      RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
  })
  
  export class SurveyRoutingModule { } 