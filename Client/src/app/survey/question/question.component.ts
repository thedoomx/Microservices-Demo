import { Component, ElementRef, EventEmitter, OnInit, Output, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from 'ngx-strongly-typed-forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestionType } from '../models/questionType.model';
import { SurveyService } from '../survey.service';
import { Question } from '../models/question.model';
import { QuestionItem } from '../models/questionItem.model';
import { questionTypeConstants } from '../constants/question-types.constants';


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
  showQuestionOptions: boolean = false;

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

    let questionItem: QuestionItem = {
      description: valueInput
    };

    this.questionItems.push(questionItem);
    this.questionForm.patchValue({
      questionItems: this.questionItems,
    });

    this.canAddQuestionItem = false;

    this.newQuestionItem.nativeElement.value = '';
  }

  removeOption(index: number) {
     this.questionItems.splice(index,1);
  }

  onQuestionTypeChange($event) {
    let selectedItemText = this.questionTypes[$event.target.selectedIndex].type;
    
    if(selectedItemText == questionTypeConstants.freeText) {
      this.questionForm.get('questionItems').clearValidators();
      this.questionForm.get('questionItems').updateValueAndValidity();
      this.showQuestionOptions = false;
      this.questionItems = [];
    }
    else {
      this.questionForm.get('questionItems').setValidators([Validators.required]);
      this.questionForm.get('questionItems').updateValueAndValidity();
      this.showQuestionOptions = true;
    }
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
