using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Oxygen.Survey.Application.Queries.Common;

namespace Oxygen.Survey.Application.Survey.Queries.Search
{
    public class SearchSurveysQuery : IRequest<IEnumerable<SurveyOutputModel>>
    {
        public class SearchSurveysQueryHandler : IRequestHandler<SearchSurveysQuery, IEnumerable<SurveyOutputModel>>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SearchSurveysQueryHandler(ISurveyQueryRepository surveyRepository)
                => this._surveyRepository = surveyRepository;

            public async Task<IEnumerable<SurveyOutputModel>> Handle(
                SearchSurveysQuery request,
                CancellationToken cancellationToken)
                => await this._surveyRepository.GetAll(cancellationToken);
        }
    }
}
