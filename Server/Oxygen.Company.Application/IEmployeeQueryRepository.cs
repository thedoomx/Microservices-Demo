namespace Oxygen.Company.Application
{
    using Oxygen.Application.Common.Contracts;
    using Oxygen.Company.Application.Department.Queries.Common;
    using Oxygen.Company.Application.Employee.Queries.Common;
    using Oxygen.Company.Application.JobTitle.Queries.Common;
    using Oxygen.Company.Application.Office.Queries.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEmployeeQueryRepository : IQueryRepository<Domain.Models.Employee>
    {
        Task<IEnumerable<EmployeeOutputModel>> GetEmployees(
            CancellationToken cancellationToken = default);

        Task<IEnumerable<DepartmentOutputModel>> GetDepartments(
            CancellationToken cancellationToken = default);

        Task<IEnumerable<OfficeOutputModel>> GetOffices(
            CancellationToken cancellationToken = default);

        Task<IEnumerable<JobTitleOutputModel>> GetJobTitles(
            CancellationToken cancellationToken = default);

        Task<EmployeeOutputModel> GetEmployeeDetails(int id, CancellationToken cancellationToken = default);

        Task<DepartmentOutputModel> GetDepartmentDetails(int id, CancellationToken cancellationToken = default);

        Task<OfficeOutputModel> GetOfficeDetails(int id, CancellationToken cancellationToken = default);
        
        Task<JobTitleOutputModel> GetJobTitleDetails(int id, CancellationToken cancellationToken = default);
    }
}
