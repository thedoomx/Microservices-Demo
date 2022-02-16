namespace Oxygen.Survey.Application.Survey.Queries.Submit
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Application.Queries.Common;

    public class SubmitSurveyQuery : EntityCommand<int>, IRequest<SubmitSurveyOutputModel>
    {
        public class SubmitSurveyQueryHandler : IRequestHandler<SubmitSurveyQuery, SubmitSurveyOutputModel>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SubmitSurveyQueryHandler(
                ISurveyQueryRepository surveyRepository)
            {
                this._surveyRepository = surveyRepository;
            }

            public async Task<SubmitSurveyOutputModel> Handle(
                SubmitSurveyQuery request,
                CancellationToken cancellationToken)
            {
                var surveyDetails = await this._surveyRepository.GetSubmitSurveyDetails(
                    request.Id,
                    cancellationToken);

                return surveyDetails;
            }
        }
    }
}
