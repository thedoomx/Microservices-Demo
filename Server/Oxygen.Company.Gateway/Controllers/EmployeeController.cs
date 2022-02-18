namespace Oxygen.Company.Gateway.Controllers
{
	using System.Linq;
	using System.Threading.Tasks;
	using AutoMapper;
	using Microsoft.AspNetCore.Mvc;
	using Oxygen.Company.Gateway.Models.Company;
	using Oxygen.Company.Gateway.Services.Company;
	using Oxygen.Company.Gateway.Services.Identity;
	using Oxygen.Company.Gateway.Services.Survey;
	using Oxygen.Web.Common;

	public class EmployeeController : ApiController
	{
		private readonly IIdentityService _identityService;
		private readonly ICompanyService _companyService;
		private readonly ISurveyService _surveyService;

		private readonly IMapper mapper;

		public EmployeeController(
			IIdentityService identityService,
			ICompanyService companyService,
			ISurveyService surveyService,
			IMapper mapper)
		{
			this._identityService = identityService;
			this._companyService = companyService;
			this._surveyService = surveyService;
			this.mapper = mapper;
		}

		[HttpGet]
		[Route(nameof(GetFullData))]
		public async Task<FullEmployeeDataOutputModel> GetFullData(int employeeId)
		{
			var getSurveysTask = Task.Run(() => this._surveyService.SearchMine(employeeId));

			var employee = await this._companyService.Get(employeeId);
			var user = await this._identityService.GetDetails(employee.UserId);

			var result = this.mapper.Map<FullEmployeeDataOutputModel>(employee);
			var surveys = await getSurveysTask;
			result.TotalSurveys = surveys.Count();
			result.SubmittedSurveys = surveys.Where(x => x.IsSubmitted == true).Count();
			result.Email = user.Email;

			return result;
		}
	}
}
