namespace Oxygen.Survey.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveyCommand;
    using Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveysCommand;
    using Oxygen.Survey.Application.EmployeeSurvey.Queries.Common;
    using Oxygen.Survey.Application.EmployeeSurvey.Queries.Details;
	using Oxygen.Web.Common;
    using System.Threading.Tasks;

    public class EmployeeSurveyController : ApiController
    {
        [HttpGet]
        [Route(nameof(GetEmployeeSurveyDetails))]
        public async Task<ActionResult<EmployeeSurveyOutputModel>> GetEmployeeSurveyDetails(
           [FromQuery] EmployeeSurveyDetailsQuery query)
           => await this.Send(query);

        [HttpPost]
        [Route(nameof(SubmitEmployeeSurvey))]
        public async Task<ActionResult<CreateEmployeeSurveyAnswersOutputModel>> SubmitEmployeeSurvey(
           [FromBody] CreateEmployeeSurveyAnswersCommand command)
           => await this.Send(command);

        [HttpPost]
        [Route(nameof(CreateEmployeesSurveys))]
        public async Task<ActionResult<CreateEmployeesSurveysOutputModel>> CreateEmployeesSurveys(
           [FromBody] CreateEmployeesSurveysCommand command)
           => await this.Send(command);
    }
}
