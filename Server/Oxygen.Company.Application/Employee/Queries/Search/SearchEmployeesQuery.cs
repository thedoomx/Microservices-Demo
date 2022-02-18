namespace Oxygen.Company.Application.Employee.Queries.Search
{
	using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Employee.Queries.Common;

    public class SearchEmployeesQuery : IRequest<IEnumerable<EmployeeOutputModel>>
    {
        public class SearchEmployeesQueryHandler : IRequestHandler<SearchEmployeesQuery, IEnumerable<EmployeeOutputModel>>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public SearchEmployeesQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<IEnumerable<EmployeeOutputModel>> Handle(
                SearchEmployeesQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetEmployees(cancellationToken);
        }
    }
}
