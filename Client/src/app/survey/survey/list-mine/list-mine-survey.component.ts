import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Survey } from '../../models/survey.model';
import { SurveyService } from '../../survey.service';

@Component({
  selector: 'app-survey-list-mine',
  templateUrl: './list-mine-survey.component.html',
  styleUrls: ['./list-mine-survey.component.css']
})
export class ListMineSurveyComponent implements OnInit {
  surveys: Array<Survey>;
  id: string;
  employeeId: string;

  constructor(private surveyService: SurveyService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.employeeId = localStorage.getItem('employeeId');

    if(this.employeeId) {
      this.fetchSurveys()
    }
  }

  fetchSurveys() {
    this.surveyService.searchMine(this.employeeId).subscribe(surveys => {
      this.surveys = surveys;
    })
  }

}
