export interface Question {
    id?: number;
    description: string;
    isRequired: boolean;
    questionType: number;
    survey: number;
}