import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/company/models/employee.model';
import { SurveyService } from '../../survey.service';
import { CompanyService } from 'src/app/company/company.service';
import { Survey } from '../../models/survey.model';
import { AssignEmployeesSurveys } from '../../models/assignEmployeesSurveys.model';
import { AssignEmployee } from '../../models/assignEmployee.model';

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

    constructor(
        private route: ActivatedRoute,
        private surveyService: SurveyService,
        private companyService: CompanyService,
        private fb: FormBuilder,
        private router: Router) {

          this.surveyForm = this.fb.group({
            name: ['', Validators.required],
            summary: ['', Validators.required],
          })

        this.id = this.route.snapshot.paramMap.get('id');

        this.submitModel = {} as AssignEmployeesSurveys;
        this.submitModel.id = Number(this.id);
    }

    fetchSurvey() {
        this.surveyService.getSurvey(this.id).subscribe(survey => {
            this.surveyName = survey.name;

          this.surveyForm = this.fb.group({
            name: [survey.name, Validators.required],
            summary: [survey.summary, Validators.required],
          })
        })
      }

    ngOnInit(): void {
        this.fetchSurvey()
    }

    onClickSubmit() {
      this.surveyService.createEmployeesSurveys(this.submitModel).subscribe((res) => {
        this.router.navigate(['survey']);
    });
    }
}