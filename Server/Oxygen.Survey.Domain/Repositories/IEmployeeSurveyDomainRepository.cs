namespace Oxygen.Survey.Domain.Repositories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEmployeeSurveyDomainRepository : IDomainRepository<EmployeeSurvey>
    {
        Task AddEmployeeSurveyAnswer(EmployeeSurveyAnswer employeeSurveyAnswer,
            CancellationToken cancellationToken = default);

        Task<EmployeeSurvey> GetById(int id,
            CancellationToken cancellationToken = default);

        Task<EmployeeSurvey> GetByEmployeeIdAndSurveyId(int employeeId, int surveyId,
           CancellationToken cancellationToken = default);
    }
}
