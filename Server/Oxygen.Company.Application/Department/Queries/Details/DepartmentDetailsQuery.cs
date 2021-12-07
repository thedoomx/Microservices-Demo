namespace Oxygen.Company.Application.Department.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Department.Queries.Common;

    public class DepartmentDetailsQuery : IRequest<DepartmentOutputModel>
    {
        public int Id { get; set; }

        public class DepartmentDetailsQueryHandler : IRequestHandler<DepartmentDetailsQuery, DepartmentOutputModel>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public DepartmentDetailsQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<DepartmentOutputModel> Handle(
                DepartmentDetailsQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetDepartmentDetails(request.Id, cancellationToken);
        }
    }
}
