import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionComponent } from './question/question.component';
import { AssignEmployeesComponent } from './survey/assign-employees/assign-employees.component';
import { CreateSurveyComponent } from './survey/create/create-survey.component';
import { ListMineSurveyComponent } from './survey/list-mine/list-mine-survey.component';
import { ListSurveyComponent } from './survey/list/list-survey.component';
import { SubmitComponent } from './survey/submit/submit.component';
import { SurveyComponent } from './survey/survey.component';

const routes: Routes = [
    { path:'question', component: QuestionComponent, },
    { path:'create-survey', component: CreateSurveyComponent, },
    { path:'list-survey', component: ListSurveyComponent, },
    { path:'list-mine-survey', component: ListMineSurveyComponent, },
    { path:'', component: SurveyComponent, },
    { path:':id/assign-employees', component: AssignEmployeesComponent, },
    { path:':id/submit-survey', component: SubmitComponent, },

  ];
  
  @NgModule({
    imports: [
      RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
  })
  
  export class SurveyRoutingModule { } 