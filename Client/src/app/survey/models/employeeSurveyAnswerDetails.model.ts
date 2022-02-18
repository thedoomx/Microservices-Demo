import { Question } from "./question.model";
import { QuestionAnswer } from "./questionAnswer.model";

export interface EmployeeSurveyAnswerDetails {
    id: number;
    question: Question;
    boolValue?: boolean;
    textValue: string;
    questionAnswer: QuestionAnswer;
}