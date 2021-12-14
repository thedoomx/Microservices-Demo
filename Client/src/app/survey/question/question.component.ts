import { Component, ElementRef, EventEmitter, OnInit, Output, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestionType } from '../models/questionType.model';
import { SurveyService } from '../survey.service';
import { Question } from '../models/question.model';
import { QuestionItem } from '../models/questionItem.model';

@Component({
  selector: 'app-survey-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @ViewChild('newQuestionItem') newQuestionItem:ElementRef;
  questionForm: FormGroup<Question>;
  questionTypes: Array<QuestionType>;
  questionItems: Array<QuestionItem> = [];

  canAddQuestionItem: boolean;

  @Output() addQuestionEventEmitter = new EventEmitter<Question>();

  constructor(
    private surveyService: SurveyService,
    private fb: FormBuilder,
    private router: Router) {

      this.surveyService.getQuestionTypes().subscribe(res => {
        this.questionTypes = res;
      });

  }

  ngOnInit(): void {
    this.questionForm = this.fb.group<Question>({
      id: [null],
      survey: [null],
      description: ['', Validators.required],
      isRequired: [false, Validators.required],
      questionType: [null, Validators.required],
      questionItems: [null, Validators.required]
    })
  }

  submit() {
    var result = this.questionForm.value;
    this.addQuestionEventEmitter.emit(result);
  }

  addOption() {
    const valueInput = this.newQuestionItem.nativeElement.value;
    
    this.questionItems.push(valueInput);
    this.questionForm.patchValue({
      questionItems: this.questionItems,
    });

    this.canAddQuestionItem = false;

    this.newQuestionItem.nativeElement.value = '';
  }

  removeOption(index: number) {
     this.questionItems.splice(index,1);
  }

  onNewQuestionItemChange(searchValue: string): void {  
    if(searchValue.length > 0) {
      this.canAddQuestionItem = true;
    }
    else {
      this.canAddQuestionItem = false;
    }
  }

}
