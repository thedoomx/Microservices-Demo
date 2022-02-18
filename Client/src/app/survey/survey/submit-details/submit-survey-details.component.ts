import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyService } from '../../survey.service';
import { CompanyService } from 'src/app/company/company.service';
import { questionTypeConstants } from '../../constants/question-types.constants';
import { SubmitQuestion } from '../../models/submitQuestion.model';
import { SubmitEmployeeSurvey } from '../../models/submitEmployeeSurvey.model';
import { SubmitEmployeeSurveyAnswer } from '../../models/submitEmployeeSurveyAnswer.model';
import { EmployeeSurveyService } from '../../employee-survey.service';
import { EmployeeSurveyDetails } from '../../models/employeeSurveyDetails.model';

@Component({
  selector: 'app-survey-submit-survey-details',
  templateUrl: './submit-survey-details.component.html',
  styleUrls: ['./submit-survey-details.component.css']
})
export class SubmitSurveyDetailsComponent implements OnInit {
  id: string;
  employeeSurveyLoaded: boolean = false;
  employeeSurvey: EmployeeSurveyDetails;

  freeTextType: string = questionTypeConstants.freeText;
  radioType: string = questionTypeConstants.radio;
  checkboxType: string = questionTypeConstants.checkbox;

  constructor(
    private route: ActivatedRoute,
    private employeeSurveyService: EmployeeSurveyService,
    private fb: FormBuilder,
    private router: Router) {

    this.id = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    this.fetchEmployeeSurvey()
  }

  fetchEmployeeSurvey() {
    this.employeeSurveyService.getEmployeeSurveyDetails(this.id).subscribe(employeeSurvey => {
      this.employeeSurvey = employeeSurvey;
      debugger;
      this.employeeSurveyLoaded = true;
    })
  }

}