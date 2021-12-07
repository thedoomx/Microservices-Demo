namespace Oxygen.Company.Application.Office.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Office.Queries.Common;

    public class OfficeDetailsQuery : IRequest<OfficeOutputModel>
    {
        public int Id { get; set; }

        public class EmployeeDetailsQueryHandler : IRequestHandler<OfficeDetailsQuery, OfficeOutputModel>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public EmployeeDetailsQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<OfficeOutputModel> Handle(
                OfficeDetailsQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetOfficeDetails(request.Id, cancellationToken);
        }
    }
}
