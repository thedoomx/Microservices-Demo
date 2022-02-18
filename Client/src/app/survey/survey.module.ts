import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { SurveyRoutingModule } from './survey-routing.module';
import { SurveyService } from './survey.service';
import { QuestionComponent } from './question/question.component';
import { SurveyComponent } from './survey/survey.component';
import { CreateSurveyComponent } from './survey/create/create-survey.component';
import { ListSurveyComponent } from './survey/list/list-survey.component';
import { AssignEmployeesComponent } from './survey/assign-employees/assign-employees.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ListMineSurveyComponent } from './survey/list-mine/list-mine-survey.component';
import { SubmitSurveyComponent } from './survey/submit/submit-survey.component';
import { EmployeeSurveyService } from './employee-survey.service';
import { SubmitSurveyDetailsComponent } from './survey/submit-details/submit-survey-details.component';


@NgModule({
  declarations: [
    QuestionComponent,
    SurveyComponent,
    CreateSurveyComponent,
    ListSurveyComponent,
    ListMineSurveyComponent,
    AssignEmployeesComponent,
    SubmitSurveyComponent,
    SubmitSurveyDetailsComponent
  ],
  providers: [
    SurveyService,
    EmployeeSurveyService
  ],
  imports: [
    CommonModule,
    SharedModule,
    SurveyRoutingModule,
    NgMultiSelectDropDownModule,
  ],
})

export class SurveyModule { }
