namespace Oxygen.Survey.Application.SurveyType.Queries.Search
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Survey.Application.SurveyType.Queries.Common;

    public class SearchSurveyTypesQuery : IRequest<IEnumerable<SurveyTypeOutputModel>>
    {
        public class SearchSurveyTypesQueryHandler : IRequestHandler<SearchSurveyTypesQuery, IEnumerable<SurveyTypeOutputModel>>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SearchSurveyTypesQueryHandler(ISurveyQueryRepository surveyRepository)
                => this._surveyRepository = surveyRepository;

            public async Task<IEnumerable<SurveyTypeOutputModel>> Handle(
                SearchSurveyTypesQuery request,
                CancellationToken cancellationToken)
                => await this._surveyRepository.SearchSurveyTypes(cancellationToken);
        }
    }
}
