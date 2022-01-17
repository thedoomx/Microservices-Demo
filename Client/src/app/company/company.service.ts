import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Employee } from './models/employee.model';
import { Department } from './models/department.model';
import { JobTitle } from './models/jobTitle.model';
import { Office } from './models/office.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  companyPath: string = environment.companyApiUrl + 'company/';
  companyPathWithoutSlash  = this.companyPath.slice(0, -1);

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Array<Employee>> {
    return this.http.get<Array<Employee>>(this.companyPath);
  }


  getCar(id: string): Observable<Employee> {
    return this.http.get<Employee>(this.companyPath + id);
  }

  editCar(id: string, car: Employee): Observable<Employee> {
    return this.http.put<Employee>(this.companyPath + id, car);
  }

  getDepartments(): Observable<Array<Department>> {
    return this.http.get<Array<Department>>(this.companyPath + 'getdepartments')
  }

  getJobTitles(): Observable<Array<JobTitle>> {
    return this.http.get<Array<JobTitle>>(this.companyPath + 'getjobTitles')
  }

  getOffices(): Observable<Array<Office>> {
    return this.http.get<Array<Office>>(this.companyPath + 'getoffices')
  }

  search(queryString): Observable<Array<Employee>> {
    return this.http.get<Array<Employee>>(this.companyPathWithoutSlash + queryString)
  }

  sort(queryString): Observable<Array<Employee>> {
    return this.http.get<Array<Employee>>(this.companyPathWithoutSlash + queryString)
  }
}
