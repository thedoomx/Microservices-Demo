namespace Oxygen.Survey.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Oxygen.Survey.Application.Queries.Common;
    using Oxygen.Survey.Application.Queries.Mine;
    using Oxygen.Survey.Application.QuestionType.Queries.Common;
    using Oxygen.Survey.Application.QuestionType.Queries.Search;
    using Oxygen.Survey.Application.Survey.Commands.Create;
    using Oxygen.Survey.Application.SurveyType.Queries.Common;
    using Oxygen.Survey.Application.SurveyType.Queries.Search;
    using Oxygen.Survey.Application.UserSurveys.Commands.Create;
    using Oxygen.Web.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class SurveyController : ApiController
    {
        [HttpGet]
        [Route(nameof(Test))]
        public async Task<ActionResult<int>> Test()
        {
            var a = new CreateSurveyCommand();
            a.Name = "Test";
            a.Summary = "Summary";
            a.SurveyType = 1;

            return 5;
        }

        [HttpPost]
        public async Task<ActionResult<CreateSurveyOutputModel>> Create(
            CreateSurveyCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(nameof(Search))]
        public async Task<ActionResult<IEnumerable<SurveyOutputModel>>> Search(
            [FromQuery] MineSurveysQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(UserSurveys))]
        public async Task<ActionResult<IEnumerable<CreateUsersSurveysCommand>>> UserSurveys(
           [FromQuery] CreateUsersSurveysCommand command)
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
