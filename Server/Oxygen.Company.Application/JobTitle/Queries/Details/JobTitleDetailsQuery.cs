namespace Oxygen.Company.Application.JobTitle.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.JobTitle.Queries.Common;

    public class JobTitleDetailsQuery : IRequest<JobTitleOutputModel>
    {
        public int Id { get; set; }

        public class JobTitleDetailsQueryHandler : IRequestHandler<JobTitleDetailsQuery, JobTitleOutputModel>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public JobTitleDetailsQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<JobTitleOutputModel> Handle(
                JobTitleDetailsQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetJobTitleDetails(request.Id, cancellationToken);
        }
    }
}
