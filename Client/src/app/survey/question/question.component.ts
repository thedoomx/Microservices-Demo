import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestionType } from '../models/questionType.model';
import { SurveyService } from '../survey.service';
import { QuestionForm, QuestionItemForm } from './questionForm.model';

@Component({
  selector: 'app-survey-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @ViewChild('newQuestionItem') newQuestionItem:ElementRef;
  questionForm: FormGroup<QuestionForm>;
  questionTypes: Array<QuestionType>;
  questionItems: Array<QuestionItemForm> = [];

  constructor(
    private surveyService: SurveyService,
    private fb: FormBuilder,
    private router: Router) {

      this.surveyService.getQuestionTypes().subscribe(res => {
        this.questionTypes = res;
      });

  }

  ngOnInit(): void {
    this.questionForm = this.fb.group<QuestionForm>({
      description: ['', Validators.required],
      isRequired: [false, Validators.required],
      questionType: [null, Validators.required],
      questionItems: [null, Validators.required]
    })
  }

  addOption() {
    debugger;
    const valueInput = this.newQuestionItem.nativeElement.value;
    this.questionItems.push(valueInput);
    this.newQuestionItem.nativeElement.value = '';
  }

  removeOption(index: number) {
     this.questionItems.splice(index,1);
  }

}
