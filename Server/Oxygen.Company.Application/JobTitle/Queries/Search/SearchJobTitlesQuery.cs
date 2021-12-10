namespace Oxygen.Company.Application.JobTitle.Queries.Search
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.JobTitle.Queries.Common;

    public class SearchJobTitlesQuery : IRequest<IEnumerable<JobTitleOutputModel>>
    {
        public class SearchJobTitlesQueryHandler : IRequestHandler<SearchJobTitlesQuery, IEnumerable<JobTitleOutputModel>>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public SearchJobTitlesQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<IEnumerable<JobTitleOutputModel>> Handle(
                SearchJobTitlesQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetJobTitles(cancellationToken);
        }
    }
}
