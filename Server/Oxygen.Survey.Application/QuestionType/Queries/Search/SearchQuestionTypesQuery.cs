namespace Oxygen.Survey.Application.QuestionType.Queries.Search
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Survey.Application.QuestionType.Queries.Common;

    public class SearchQuestionTypesQuery : IRequest<IEnumerable<QuestionTypeOutputModel>>
    {
        public class SearchQuestionTypesQueryHandler : IRequestHandler<SearchQuestionTypesQuery, IEnumerable<QuestionTypeOutputModel>>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SearchQuestionTypesQueryHandler(ISurveyQueryRepository surveyRepository)
                => this._surveyRepository = surveyRepository;

            public async Task<IEnumerable<QuestionTypeOutputModel>> Handle(
                SearchQuestionTypesQuery request,
                CancellationToken cancellationToken)
                => await this._surveyRepository.SearchQuestionTypes(cancellationToken);
        }
    }
}
