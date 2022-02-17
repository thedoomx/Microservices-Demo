namespace Oxygen.Survey.Application.Survey.Queries.GetSubmit
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Application.Common;

    public class GetSubmitSurveyQuery : EntityCommand<int>, IRequest<GetSubmitSurveyOutputModel>
    {
        public class SubmitSurveyQueryHandler : IRequestHandler<GetSubmitSurveyQuery, GetSubmitSurveyOutputModel>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SubmitSurveyQueryHandler(
                ISurveyQueryRepository surveyRepository)
            {
                this._surveyRepository = surveyRepository;
            }

            public async Task<GetSubmitSurveyOutputModel> Handle(
                GetSubmitSurveyQuery request,
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
