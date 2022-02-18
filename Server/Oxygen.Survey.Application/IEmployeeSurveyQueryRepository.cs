namespace Oxygen.Survey.Application
{
    using Oxygen.Application.Common.Contracts;
    using System.Threading;
    using System.Threading.Tasks;
	using Oxygen.Survey.Application.EmployeeSurvey.Queries.Common;

	public interface IEmployeeSurveyQueryRepository : IQueryRepository<Domain.Models.EmployeeSurvey>
    {
        Task<EmployeeSurveyOutputModel> GetEmployeeSurveyDetails(int employeeSurveyId, CancellationToken cancellationToken = default);
    }
}
