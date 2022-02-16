import { Component, ElementRef, EventEmitter, OnInit, Output, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestionType } from '../models/questionType.model';
import { SurveyService } from '../survey.service';
import { Question } from '../models/question.model';
import { QuestionAnswer } from '../models/questionAnswer.model';
import { questionTypeConstants } from '../constants/question-types.constants';


@Component({
  selector: 'app-survey-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @ViewChild('newQuestionAnswer') newQuestionAnswer:ElementRef;
  questionForm: FormGroup<Question>;
  questionTypes: Array<QuestionType>;
  questionAnswers: Array<QuestionAnswer> = [];
  showQuestionOptions: boolean = false;

  canAddQuestionAnswer: boolean;

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
      description: ['', Validators.required],
      isRequired: [false, Validators.required],
      questionType: [null, Validators.required],
      questionAnswers: [[], Validators.required]
    })
  }

  submit() {
    var result = this.questionForm.value;
    this.addQuestionEventEmitter.emit(result);
  }

  addOption() {
    const valueInput = this.newQuestionAnswer.nativeElement.value;

    let questionAnswer: QuestionAnswer = {
      description: valueInput
    };

    this.questionAnswers.push(questionAnswer);
    this.questionForm.patchValue({
      questionAnswers: this.questionAnswers,
    });

    this.canAddQuestionAnswer = false;

    this.newQuestionAnswer.nativeElement.value = '';
  }

  removeOption(index: number) {
     this.questionAnswers.splice(index,1);
  }

  onQuestionTypeChange($event) {
    let selectedItemText = this.questionTypes[$event.target.selectedIndex].type;
    
    if(selectedItemText == questionTypeConstants.freeText) {
      this.questionForm.get('questionAnswers').clearValidators();
      this.questionForm.get('questionAnswers').updateValueAndValidity();
      this.showQuestionOptions = false;
      this.questionAnswers = [];
    }
    else {
      this.questionForm.get('questionAnswers').setValidators([Validators.required]);
      this.questionForm.get('questionAnswers').updateValueAndValidity();
      this.showQuestionOptions = true;
    }
  }

  onNewQuestionAnswerChange(searchValue: string): void {  
    if(searchValue.length > 0) {
      this.canAddQuestionAnswer = true;
    }
    else {
      this.canAddQuestionAnswer = false;
    }
  }

}
