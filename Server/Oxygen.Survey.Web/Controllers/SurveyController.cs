﻿namespace Oxygen.Survey.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
	using Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveysCommand;
	using Oxygen.Survey.Application.Queries.Common;
    using Oxygen.Survey.Application.Queries.Mine;
    using Oxygen.Survey.Application.QuestionType.Queries.Common;
    using Oxygen.Survey.Application.QuestionType.Queries.Search;
    using Oxygen.Survey.Application.Survey.Commands.Create;
	using Oxygen.Survey.Application.Survey.Queries.Details;
	using Oxygen.Survey.Application.Survey.Queries.Search;
	using Oxygen.Survey.Application.SurveyType.Queries.Common;
    using Oxygen.Survey.Application.SurveyType.Queries.Search;
    using Oxygen.Web.Common;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SurveyController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<SurveyOutputModel>> GetSurveyDetails(
            [FromRoute] SurveyDetailsQuery query)
            => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<CreateSurveyOutputModel>> Create(
            CreateSurveyCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(nameof(SearchAll))]
        public async Task<ActionResult<IEnumerable<SurveyOutputModel>>> SearchAll(
            [FromQuery] SearchSurveysQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(Search))]
        public async Task<ActionResult<IEnumerable<SurveyOutputModel>>> Search(
            [FromQuery] MineSurveysQuery query)
            => await this.Send(query);

        [HttpPost]
        [Route(nameof(CreateEmployeesSurveys))]
        public async Task<ActionResult<CreateEmployeesSurveysOutputModel>> CreateEmployeesSurveys(
           [FromBody] CreateEmployeesSurveysCommand command)
           => await this.Send(command);

        [HttpGet]
        [Route(nameof(GetSurveyTypes))]
        public async Task<ActionResult<IEnumerable<SurveyTypeOutputModel>>> GetSurveyTypes(
            [FromQuery] SearchSurveyTypesQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(GetQuestionTypes))]
        public async Task<ActionResult<IEnumerable<QuestionTypeOutputModel>>> GetQuestionTypes(
            [FromQuery] SearchQuestionTypesQuery query)
            => await this.Send(query);
    }
}
