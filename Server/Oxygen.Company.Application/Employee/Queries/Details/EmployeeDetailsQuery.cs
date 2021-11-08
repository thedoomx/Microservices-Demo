namespace Oxygen.Company.Application.Employee.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Employee.Queries.Common;

    public class EmployeeDetailsQuery : IRequest<EmployeeOutputModel>
    {
        public int Id { get; set; }

        public class EmployeeDetailsQueryHandler : IRequestHandler<EmployeeDetailsQuery, EmployeeOutputModel>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public EmployeeDetailsQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<EmployeeOutputModel> Handle(
                EmployeeDetailsQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetEmployeeDetails(request.Id, cancellationToken);
        }
    }
}
