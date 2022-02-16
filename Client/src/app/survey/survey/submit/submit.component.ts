import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/company/models/employee.model';
import { SurveyService } from '../../survey.service';
import { CompanyService } from 'src/app/company/company.service';
import { Survey } from '../../models/survey.model';
import { AssignEmployeesSurveys } from '../../models/assignEmployeesSurveys.model';
import { AssignEmployee } from '../../models/assignEmployee.model';
import { questionTypeConstants } from '../../constants/question-types.constants';
import { SubmitQuestion } from '../../models/submitQuestion.model';

@Component({
    selector: 'app-survey-submit',
    templateUrl: './submit.component.html',
    styleUrls: ['./submit.component.css']
})
export class SubmitComponent implements OnInit {
    id: string;
    surveyForm:  FormGroup;
    surveyName: string;
    submitModel: AssignEmployeesSurveys;
    surveyLoaded: boolean = false;
    questions: Array<SubmitQuestion>;

    freeTextType: string = questionTypeConstants.freeText;
    radioType: string = questionTypeConstants.radio;
    checkboxType: string = questionTypeConstants.checkbox;

    constructor(
        private route: ActivatedRoute,
        private surveyService: SurveyService,
        private companyService: CompanyService,
        private fb: FormBuilder,
        private router: Router) {
          
          this.surveyForm = this.fb.group({
            name: ['', Validators.required],
            summary: ['', Validators.required],
            questions: [[], Validators.required]
          })

        this.id = this.route.snapshot.paramMap.get('id');

        // this.submitModel = {} as AssignEmployeesSurveys;
        // this.submitModel.id = Number(this.id);
    }

    fetchSubmitSurvey() {
        this.surveyService.getSubmitSurveyDetails(this.id).subscribe(survey => {
            this.surveyLoaded = true;
            this.questions = survey.questions;

          this.surveyForm = this.fb.group({
            name: [survey.name, Validators.required],
            summary: [survey.summary, Validators.required],
            questions: [survey.questions, Validators.required],
          })
        })
      }

    ngOnInit(): void {
        this.fetchSubmitSurvey()
    }

    onClickSubmit() {
      // this.surveyService.createEmployeesSurveys(this.submitModel).subscribe((res) => {
      //   this.router.navigate(['survey']);
      // });
    }
}