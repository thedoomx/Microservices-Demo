namespace Oxygen.Company.Gateway.Services.Survey
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Oxygen.Company.Gateway.Models.Survey;
	using Refit;

    public interface ISurveyService
    {
        [Get("/Survey/SearchMine?EmployeeId={Id}")]
        Task<IEnumerable<MineSurveysOutputModel>> SearchMine(int Id);
    }
}
