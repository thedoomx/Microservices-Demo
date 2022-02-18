import { QuestionAnswer } from "./questionAnswer.model";

export interface Question {
    id?: number;
    description: string;
    isRequired: boolean;
    questionType: number;
    survey?: number;
    questionAnswers: Array<QuestionAnswer>
}