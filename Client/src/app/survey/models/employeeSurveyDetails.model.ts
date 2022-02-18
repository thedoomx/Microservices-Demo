import { SubmitSurvey } from "./submitSurvey.model";
import { EmployeeSurveyAnswerDetails } from "./employeeSurveyAnswerDetails.model";

export interface EmployeeSurveyDetails {
    id: number;
    survey: SubmitSurvey;
    employeeSurveyAnswers: Array<EmployeeSurveyAnswerDetails> ;
}