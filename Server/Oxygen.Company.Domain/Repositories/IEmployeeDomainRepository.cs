namespace Oxygen.Company.Domain.Repositories
{
    using Oxygen.Domain.Common;
    using System.Threading.Tasks;
    using Oxygen.Company.Domain.Models;
    using System.Threading;

    public interface IEmployeeDomainRepository : IDomainRepository<Employee>
    {
        Task<int> GetEmployeeIdByUserId(string userId, CancellationToken cancellationToken = default);

        Task SaveDepartment(Department department, CancellationToken cancellationToken = default);

        Task SaveJobTitle(JobTitle jobTitle, CancellationToken cancellationToken = default);

        Task SaveOffice(Office office, CancellationToken cancellationToken = default);
    }
}
