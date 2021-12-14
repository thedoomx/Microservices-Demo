import { Question } from "./question.model";

export interface Survey {
    id?: number;
    name: string;
    summary: string;
    surveyType: number;
    questions: Array<Question>
}