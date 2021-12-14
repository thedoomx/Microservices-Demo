import { QuestionItem } from "./questionItem.model";

export interface Question {
    id?: number;
    description: string;
    isRequired: boolean;
    questionType: number;
    survey?: number;
    questionItems: Array<QuestionItem>
}