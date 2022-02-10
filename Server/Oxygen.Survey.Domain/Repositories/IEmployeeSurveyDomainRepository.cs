namespace Oxygen.Survey.Domain.Repositories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEmployeeSurveyDomainRepository : IDomainRepository<EmployeeSurvey>
    {
        Task<EmployeeSurvey> GetById(int id, CancellationToken cancellationToken = default);
    }
}
