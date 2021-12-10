import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { ProfileComponent } from './profile/profile.component';
import { SharedModule } from '../shared/shared.module';
import { SurveyRoutingModule } from './survey-routing.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
//   declarations: [ProfileComponent],
  imports: [
    CommonModule,
    SharedModule,
    SurveyRoutingModule,
    ReactiveFormsModule,
  ],
//   exports: [ProfileComponent]
})
export class SurveyModule { }
