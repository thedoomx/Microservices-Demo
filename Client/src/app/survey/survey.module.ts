import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { SurveyRoutingModule } from './survey-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SurveyService } from './survey.service';
import { QuestionComponent } from './question/question.component';

@NgModule({
  declarations: [
    QuestionComponent,
  ],
  providers: [
    SurveyService
  ],
  imports: [
    CommonModule,
    SharedModule,
    SurveyRoutingModule,
    ReactiveFormsModule,
  ],
})

export class SurveyModule { }
