export interface QuestionForm {
    id?: number;
    description: string;
    isRequired: boolean;
    questionType: number;
    survey?: number;
    questionItems: Array<QuestionItemForm>;
}

export interface QuestionItemForm {
    id?: number;
    description: string;
}