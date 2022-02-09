import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/company/models/employee.model';
import { SurveyService } from '../../survey.service';
import { CompanyService } from 'src/app/company/company.service';
import { Survey } from '../../models/survey.model';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
    selector: 'app-survey-assign-employees',
    templateUrl: './assign-employees.component.html',
    styleUrls: ['./assign-employees.component.css']
})
export class AssignEmployeesComponent implements OnInit {
    id: string;
    employees: Array<Employee>;
    surveyForm:  FormGroup;
    surveyName: string;

    selectedItems = [];
    dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'firstName',
    };

    constructor(
        private route: ActivatedRoute,
        private surveyService: SurveyService,
        private companyService: CompanyService,
        private fb: FormBuilder,
        private router: Router) {

          this.surveyForm = this.fb.group({
            name: ['', Validators.required],
            summary: ['', Validators.required],
            employeeIds: [this.selectedItems, Validators.required]
          })

        this.id = this.route.snapshot.paramMap.get('id');

        this.employees = [];

        this.companyService.getEmployees().subscribe(res => {
            this.employees = res;
        })

    }

    fetchSurvey() {
        this.surveyService.getSurvey(this.id).subscribe(survey => {
            this.surveyName = survey.name;

          this.surveyForm = this.fb.group({
            name: [survey.name, Validators.required],
            summary: [survey.summary, Validators.required],
            employeeIds: [this.selectedItems, Validators.required]
          })
        })
      }

    ngOnInit(): void {
        this.fetchSurvey()
    }

    onClickSubmit() {
    }
}