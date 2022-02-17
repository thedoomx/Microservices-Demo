namespace Oxygen.Survey.Application.Survey.Queries.Details
{
	using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
	using Oxygen.Application.Common;
    using Oxygen.Survey.Application.Queries.Common;

    public class SurveyDetailsQuery : EntityCommand<int>, IRequest<SurveyOutputModel>
    {
        public class SurveyDetailsQueryHandler : IRequestHandler<SurveyDetailsQuery, SurveyOutputModel>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public SurveyDetailsQueryHandler(
                ISurveyQueryRepository surveyRepository)
            {
                this._surveyRepository = surveyRepository;
            }

            public async Task<SurveyOutputModel> Handle(
                SurveyDetailsQuery request,
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
