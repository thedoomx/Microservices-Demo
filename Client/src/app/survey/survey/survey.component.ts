import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestionType } from '../models/questionType.model';
import { SurveyService } from '../survey.service';
import { Question } from '../models/question.model';
import { Survey } from '../models/survey.model';
import { SurveyType } from '../models/surveyType.model';
declare var $: any;

@Component({
    selector: 'app-survey-survey',
    templateUrl: './survey.component.html',
    styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {
    @ViewChild('newSurveyItem') newQSurveyItem: ElementRef;
    surveyForm: FormGroup<Survey>;
    surveyTypes: Array<SurveyType>;
    questions: Array<Question> = [];

    @ViewChild('documentFormModal') myModal;
    showQuestionModal: boolean;

    constructor(
        private surveyService: SurveyService,
        private fb: FormBuilder,
        private router: Router) {

        this.surveyService.getSurveyTypes().subscribe(res => {
            this.surveyTypes = res;
        });

    }

    ngOnInit(): void {
        this.surveyForm = this.fb.group<Survey>({
            name: ['', Validators.required],
            summary: ['', Validators.required],
            surveyType: [null, Validators.required],
            questions: [null, Validators.required]
        })
    }

    onClickSubmit() {
        var survey = this.surveyForm.value;
        this.surveyService.createSurvey(survey).subscribe((res) => {
            debugger;

        });
    }

    createQuestionForm() {
        $("#documentFormModal").modal('show');
        this.showQuestionModal = true;
    }

    addQuestion(question: Question) {
        this.questions.push(question);
        this.surveyForm.patchValue({
            questions: this.questions,
        });

        $("#documentFormModal").modal('hide');
        this.showQuestionModal = false;
    }

    removeQuestion(index: number) {
        this.questions.splice(index, 1);
    }

}
