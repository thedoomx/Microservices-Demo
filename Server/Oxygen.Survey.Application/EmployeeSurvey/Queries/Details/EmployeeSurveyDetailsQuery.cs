namespace Oxygen.Survey.Application.EmployeeSurvey.Queries.Details
{
	using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
	using Oxygen.Application.Common;
	using Oxygen.Survey.Application.EmployeeSurvey.Queries.Common;
	using Oxygen.Survey.Domain.Repositories;

	public class EmployeeSurveyDetailsQuery : EntityCommand<int>, IRequest<EmployeeSurveyOutputModel>
    {
        public class EmployeeSurveyDetailsQueryHandler : IRequestHandler<EmployeeSurveyDetailsQuery, EmployeeSurveyOutputModel>
        {
            private readonly IEmployeeSurveyQueryRepository _employeeSurveyRepository;

            public EmployeeSurveyDetailsQueryHandler(
                IEmployeeSurveyQueryRepository surveyRepository)
            {
                this._employeeSurveyRepository = surveyRepository;
            }

            public async Task<EmployeeSurveyOutputModel> Handle(
                EmployeeSurveyDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var surveyDetails = await this._employeeSurveyRepository.GetEmployeeSurveyDetails(
                    request.Id,
                    cancellationToken);

                return surveyDetails;
            }
        }
    }
}
