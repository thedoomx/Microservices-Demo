namespace Oxygen.Survey.Application.Queries.Survey.Mine
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
	using Oxygen.Survey.Application.Queries.Common;
	using Oxygen.Survey.Application.Survey.Queries.Mine;

	public class MineSurveysQuery : IRequest<IEnumerable<MineSurveysOutputModel>>
    {
        public int? EmployeeId { get; set; }

        public class MineSurveysQueryHandler : IRequestHandler<MineSurveysQuery, IEnumerable<MineSurveysOutputModel>>
        {
            private readonly ISurveyQueryRepository _surveyRepository;

            public MineSurveysQueryHandler(ISurveyQueryRepository surveyRepository)
                => this._surveyRepository = surveyRepository;

            public async Task<IEnumerable<MineSurveysOutputModel>> Handle(
                MineSurveysQuery request,
                CancellationToken cancellationToken)
                => await this._surveyRepository.GetMine(request.EmployeeId, cancellationToken);
        }
    }
}
