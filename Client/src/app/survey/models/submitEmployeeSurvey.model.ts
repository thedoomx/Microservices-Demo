import { SubmitEmployeeSurveyAnswer } from "./submitEmployeeSurveyAnswer.model";

export interface SubmitEmployeeSurvey {
    employeeId: number;
    surveyId: number;
    questionAnswers: Array<SubmitEmployeeSurveyAnswer>
}