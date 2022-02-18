import { Survey } from "./survey.model";

export interface MineSurveys {
    id: number;
    isSubmitted: boolean;
    survey: Survey,
}