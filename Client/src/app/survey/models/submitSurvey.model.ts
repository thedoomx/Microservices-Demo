import { SubmitQuestion } from "./submitQuestion.model";

export interface SubmitSurvey {
    id: number;
    name: string;
    summary: string;
    surveyType: string;
    questions: Array<SubmitQuestion>
}