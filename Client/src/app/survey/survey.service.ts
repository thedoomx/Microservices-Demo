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

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  surveyPath: string = environment.surveyApiUrl + 'survey/';
  surveyPathWithoutSlash  = this.surveyPath.slice(0, -1);

  constructor(private http: HttpClient) { }

  submitEmployeeSurvey(survey: SubmitEmployeeSurvey): Observable<number> {
    return this.http.post<number>(this.surveyPath + 'submitEmployeeSurvey', survey);
  }

  getSubmitSurveyDetails(id: string): Observable<SubmitSurvey> {
    return this.http.get<SubmitSurvey>(this.surveyPath + 'getSubmitSurveyDetails?Id=' + id)
  }

  searchAll(): Observable<Array<Survey>> {
    return this.http.get<Array<Survey>>(this.surveyPath + 'searchAll')
  }

  searchMine(employeeId: string): Observable<Array<Survey>> {
    return this.http.get<Array<Survey>>(this.surveyPath + 'searchMine?employeeId=' + employeeId)
  }

  search(): Observable<Array<Survey>> {
    return this.http.get<Array<Survey>>(this.surveyPath + 'search')
  }

  getSurvey(id: string): Observable<Survey> {
    return this.http.get<Survey>(this.surveyPath + id);
  }

  createSurvey(survey: Survey): Observable<Survey> {
    return this.http.post<Survey>(this.surveyPathWithoutSlash, survey);
  }

  createEmployeesSurveys(survey: AssignEmployeesSurveys): Observable<AssignEmployeesSurveys> {
    return this.http.post<AssignEmployeesSurveys>(this.surveyPath + 'createEmployeesSurveys', survey);
  }

  getSurveyTypes(): Observable<Array<SurveyType>> {
    return this.http.get<Array<SurveyType>>(this.surveyPath + 'getSurveyTypes')
  }

  getQuestionTypes(): Observable<Array<QuestionType>> {
    return this.http.get<Array<QuestionType>>(this.surveyPath + 'getQuestionTypes')
  }
  
}
