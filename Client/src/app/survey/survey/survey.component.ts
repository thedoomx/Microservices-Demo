import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestionType } from '../models/questionType.model';
import { SurveyService } from '../survey.service';
import { Question } from '../models/question.model';
import { Survey } from '../models/survey.model';
import { SurveyType } from '../models/surveyType.model';

@Component({
  selector: 'app-survey-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {
  @ViewChild('newSurveyItem') newQSurveyItem:ElementRef;
  surveyForm: FormGroup<Survey>;
  surveyTypes: Array<SurveyType>;
  questions: Array<Question> = [];

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
      id: [null],
      name: ['', Validators.required],
      summary: ['', Validators.required],
      surveyType: [null, Validators.required],
      questions: [null, Validators.required]
    })
  }

  createQuestionForm() {
      
  }

  addQuestion(question: Question) {
    debugger;
    //const valueInput = this.newQuestionItem.nativeElement.value;
    this.questions.push(question);
    //this.newQuestionItem.nativeElement.value = '';
  }

  removeQuestion(index: number) {
     this.questions.splice(index,1);
  }

}
