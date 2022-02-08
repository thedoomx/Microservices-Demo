import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Employee } from 'src/app/company/models/employee.model';
import { SurveyService } from '../../survey.service';
import { CompanyService } from 'src/app/company/company.service';
declare var $: any;

@Component({
    selector: 'app-survey-survey',
    templateUrl: './survey.component.html',
    styleUrls: ['./survey.component.css']
})
export class AssignEmployeesComponent implements OnInit {
    employees: Array<Employee>;

    constructor(
        private surveyService: SurveyService,
        private companyService: CompanyService,
        private fb: FormBuilder,
        private router: Router) {

        this.companyService.getEmployees().subscribe(res => {
            this.employees = res;
        })

    }

    ngOnInit(): void {
    }

    onClickSubmit() {
    }
}
