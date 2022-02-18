import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Survey } from '../../models/survey.model';
import { SurveyService } from '../../survey.service';

@Component({
  selector: 'app-survey-list',
  templateUrl: './list-survey.component.html',
  styleUrls: ['./list-survey.component.css']
})
export class ListSurveyComponent implements OnInit {
  surveys: Array<Survey>;
  id: string;

  constructor(private surveyService: SurveyService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.fetchSurveys()

  }

  fetchSurveys() {
    this.surveyService.searchAll().subscribe(surveys => {
      this.surveys = surveys;
    })
  }

  // assignCars(event) {
  //   this.cars = event['carAds'];
  // }

}
