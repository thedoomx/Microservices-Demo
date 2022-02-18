import { SubmitQuestionAnswer } from "./submitQuestionAnswer.model";

export interface SubmitQuestion {
    id: number;
    description: string;
    isRequired: boolean;
    questionType: string;
    questionAnswers: Array<SubmitQuestionAnswer>
}