import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Survey } from './models/survey.model';
import { QuestionType } from './models/questionType.model';
import { SurveyType } from './models/surveyType.model';
import { AssignEmployeesSurveys } from './models/assignEmployeesSurveys.model';
import { SubmitSurvey } from './models/submitSurvey.model';
import { SubmitEmployeeSurvey } from './models/submitEmployeeSurvey.model';
import { EmployeeSurveyDetails } from './models/employeeSurveyDetails.model';

@Injectable({
  providedIn: 'root'
})

export class EmployeeSurveyService {
  employeeSurveyPath: string = environment.surveyApiUrl + 'employeesurvey/';
  employeeSurveyPathWithoutSlash  = this.employeeSurveyPath.slice(0, -1);

  constructor(private http: HttpClient) { }

  getEmployeeSurveyDetails(id: string): Observable<EmployeeSurveyDetails> {
    return this.http.get<EmployeeSurveyDetails>(this.employeeSurveyPath + 'getEmployeeSurveyDetails?Id=' + id)
  }

  submitEmployeeSurvey(survey: SubmitEmployeeSurvey): Observable<number> {
    return this.http.post<number>(this.employeeSurveyPath + 'submitEmployeeSurvey', survey);
  }

  createEmployeesSurveys(survey: AssignEmployeesSurveys): Observable<AssignEmployeesSurveys> {
    return this.http.post<AssignEmployeesSurveys>(this.employeeSurveyPath + 'createEmployeesSurveys', survey);
  }
}
