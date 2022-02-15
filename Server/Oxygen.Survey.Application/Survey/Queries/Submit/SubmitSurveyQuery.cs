namespace Oxygen.Survey.Application.Survey.Queries.Submit
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Application.Queries.Common;

    public class SubmitSurveyQuery : EntityCommand<int>, IRequest<SurveyOutputModel>
    {
        public class SubmitSurveyQueryHandler : IRequestHandler<SubmitSurveyQuery, SurveyOutputModel>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SubmitSurveyQueryHandler(
                ISurveyQueryRepository surveyRepository)
            {
                this._surveyRepository = surveyRepository;
            }

            public async Task<SurveyOutputModel> Handle(
                SubmitSurveyQuery request,
                CancellationToken cancellationToken)
            {
                var surveyDetails = await this._surveyRepository.GetDetails(
                    request.Id,
                    cancellationToken);

                return surveyDetails;
            }
        }
    }
}
