import { Component, ElementRef, OnInit, resolveForwardRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/company/models/employee.model';
import { SurveyService } from '../../survey.service';
import { CompanyService } from 'src/app/company/company.service';
import { Survey } from '../../models/survey.model';
import { AssignEmployeesSurveys } from '../../models/assignEmployeesSurveys.model';
import { AssignEmployee } from '../../models/assignEmployee.model';
import { questionTypeConstants } from '../../constants/question-types.constants';
import { SubmitQuestion } from '../../models/submitQuestion.model';
import { SubmitEmployeeSurvey } from '../../models/submitEmployeeSurvey.model';
import { SubmitEmployeeSurveyAnswer } from '../../models/submitEmployeeSurveyAnswer.model';
import { SubmitQuestionAnswer } from '../../models/submitQuestionAnswer.model';

@Component({
  selector: 'app-survey-submit',
  templateUrl: './submit.component.html',
  styleUrls: ['./submit.component.css']
})
export class SubmitComponent implements OnInit {
  id: string;
  surveyForm: FormGroup;
  surveyName: string;
  submitModel: SubmitEmployeeSurvey;
  surveyLoaded: boolean = false;
  questions: Array<SubmitQuestion>;

  freeTextType: string = questionTypeConstants.freeText;
  radioType: string = questionTypeConstants.radio;
  checkboxType: string = questionTypeConstants.checkbox;

  constructor(
    private route: ActivatedRoute,
    private surveyService: SurveyService,
    private companyService: CompanyService,
    private fb: FormBuilder,
    private router: Router) {

    this.surveyForm = this.fb.group({
      name: ['', Validators.required],
      summary: ['', Validators.required],
      questions: [[], Validators.required],
      questionAnswers: this.fb.array([])
    })

    this.id = this.route.snapshot.paramMap.get('id');

    this.submitModel = {} as SubmitEmployeeSurvey;
    this.submitModel.employeeId = Number(localStorage.getItem('employeeId'));
    this.submitModel.surveyId = Number(this.id);
  }

  get questionAnswers() {
    return this.surveyForm.controls["questionAnswers"] as FormArray;
  }

  fetchSubmitSurvey() {
    this.surveyService.getSubmitSurveyDetails(this.id).subscribe(survey => {

      this.questions = survey.questions;

      this.surveyForm = this.fb.group({
        name: [survey.name, Validators.required],
        summary: [survey.summary, Validators.required],
        questions: [survey.questions, Validators.required],
        questionAnswers: this.fb.array([])
      })

      this.createQuestionAnswers(survey.questions);

      this.surveyLoaded = true;
    })
  }

  addQuestionAnswerToForm(question) {

    var validatorRequired = question.isRequired == true ? Validators.required : null;
    let questionAnswerForm;

    if (question.questionType == questionTypeConstants.checkbox) {
      questionAnswerForm = this.fb.group({
        boolValue: [null, validatorRequired]
      });
    }
    else if (question.questionType == questionTypeConstants.radio) {
      questionAnswerForm = this.fb.group({
        questionAnswerId: [null, validatorRequired]
      });
    }
    else if (question.questionType == questionTypeConstants.freeText) {
      questionAnswerForm = this.fb.group({
        textValue: ['', validatorRequired]
      });
    }

    this.questionAnswers.push(questionAnswerForm);
  }

  createQuestionAnswers(questions) {
    var questionAnswers = [];

    for (let question of questions) {
      this.addQuestionAnswerToForm(question);
      var questionAnswer = {} as SubmitEmployeeSurveyAnswer;

      questionAnswer.questionId = question.id;

      questionAnswers.push(questionAnswer);
    }

  }

  ngOnInit(): void {
    this.fetchSubmitSurvey()
  }

  onClickSubmit() {
    var questionAnswers = [];
    var questionAnswersValues = this.surveyForm.value.questionAnswers;

    for (let i = 0; i < questionAnswersValues.length; i++) {
      let questionAnswerValue = questionAnswersValues[i];
      let question = this.questions[i];

      var questionAnswer = {} as SubmitEmployeeSurveyAnswer;
      questionAnswer.questionId = question.id;

      if (question.questionType == questionTypeConstants.checkbox) {
        questionAnswer.boolValue = questionAnswerValue.boolValue;
      }
      else if (question.questionType == questionTypeConstants.radio) {
        questionAnswer.questionAnswerId = Number(questionAnswerValue.questionAnswerId);
      }
      else if (question.questionType == questionTypeConstants.freeText) {
        questionAnswer.textValue = questionAnswerValue.textValue;
      }

      questionAnswers.push(questionAnswer);
    }

    this.submitModel.questionAnswers = questionAnswers;

    this.surveyService.submitEmployeeSurvey(this.submitModel).subscribe((res) => {
      this.router.navigate(['survey']);
    });
  }
}