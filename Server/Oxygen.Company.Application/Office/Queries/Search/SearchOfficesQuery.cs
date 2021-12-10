namespace Oxygen.Company.Application.Office.Queries.Search
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Company.Application.Office.Queries.Common;

    public class SearchOfficesQuery : IRequest<IEnumerable<OfficeOutputModel>>
    {
        public class SearchOfficesQueryHandler : IRequestHandler<SearchOfficesQuery, IEnumerable<OfficeOutputModel>>
        {
            private readonly IEmployeeQueryRepository _employeeRepository;

            public SearchOfficesQueryHandler(IEmployeeQueryRepository employeeRepository)
                => this._employeeRepository = employeeRepository;

            public async Task<IEnumerable<OfficeOutputModel>> Handle(
                SearchOfficesQuery request,
                CancellationToken cancellationToken)
                => await this._employeeRepository.GetOffices(cancellationToken);
        }
    }
}
