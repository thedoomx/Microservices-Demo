namespace Oxygen.Survey.Application.Queries.Mine
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Survey.Application.Queries.Common;

    public class MineSurveysQuery : IRequest<IEnumerable<SurveyOutputModel>>
    {
        public string? UserId { get; set; }

        public class MineSurveysQueryHandler : IRequestHandler<MineSurveysQuery, IEnumerable<SurveyOutputModel>>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public MineSurveysQueryHandler(ISurveyQueryRepository surveyRepository)
                => this._surveyRepository = surveyRepository;

            public async Task<IEnumerable<SurveyOutputModel>> Handle(
                MineSurveysQuery request,
                CancellationToken cancellationToken)
                => await this._surveyRepository.GetMine(request.UserId, cancellationToken);
        }
    }
}
