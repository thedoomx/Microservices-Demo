import { Component, OnInit, resolveForwardRef } from '@angular/core';
import { RegisterModelForm } from './register.model';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';
import { CompanyService } from 'src/app/company/company.service';
import { JobTitle } from 'src/app/company/models/jobTitle.model';
import { Department } from 'src/app/company/models/department.model';
import { Office } from 'src/app/company/models/office.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  loginForm: FormGroup<RegisterModelForm>;
  departments: Array<Department>;
  jobTitles: Array<JobTitle>;
  offices: Array<Office>;

  constructor(
    private companyService: CompanyService,
    private fb: FormBuilder,
    private authenticationService: AuthenticationService,
    private router: Router) {
      this.companyService.getDepartments().subscribe(res => {
        this.departments = res;
      })

      this.companyService.getJobTitles().subscribe(res => {
        this.jobTitles = res;
      })

      this.companyService.getOffices().subscribe(res => {
        this.offices = res;
      })
     }

  ngOnInit(): void {
    this.loginForm = this.fb.group<RegisterModelForm>({
      email: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      surName: ['', Validators.required],
      lastName: ['', Validators.required],
      department: [null, Validators.required],
      jobTitle: [null, Validators.required],
      office: [null, Validators.required],
      phoneNumber: ['', Validators.required]
    })
  }

  login() {
    const form = this.loginForm.value;

    const { email, password, firstName, surName, lastName, department, jobTitle, office } = form;
    const userData = { email, password, firstName, surName, lastName, department, jobTitle, office };

    this.authenticationService.register(userData).subscribe(res => {
      this.authenticationService.setToken(res['token']);
      debugger;
      this.router.navigate(['']).then(() => {
        window.location.reload();
      });
    })
  }
}
