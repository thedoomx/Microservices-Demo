namespace Oxygen.Company.Application.Employee.Queries.Id
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Employee.Queries.Common;
	using Oxygen.Company.Domain.Repositories;

	public class EmployeeIdQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public class EmployeeIdQueryHandler : IRequestHandler<EmployeeIdQuery, int>
        {
            private readonly IEmployeeDomainRepository _employeeRepository;

            public EmployeeIdQueryHandler(IEmployeeDomainRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<int> Handle(
                EmployeeIdQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetEmployeeIdByUserId(request.UserId, cancellationToken);
        }
    }
}
