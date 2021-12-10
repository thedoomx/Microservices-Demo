namespace Oxygen.Company.Application.Department.Queries.Search
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Department.Queries.Common;

    public class SearchDepartmentsQuery : IRequest<IEnumerable<DepartmentOutputModel>>
    {
        public class SearchDepartmentsQueryHandler : IRequestHandler<SearchDepartmentsQuery, IEnumerable<DepartmentOutputModel>>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public SearchDepartmentsQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<IEnumerable<DepartmentOutputModel>> Handle(
                SearchDepartmentsQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetDepartments(cancellationToken);
        }
    }
}
