namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveysCommand
{
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
	using Oxygen.Infrastructure.Common.Messages.Survey;
	using System.Collections.Generic;
	using Oxygen.Survey.Application.EmployeeSurveys.Commands.Common;

	public class CreateEmployeesSurveysCommand : EntityCommand<int>, IRequest<Result<CreateEmployeesSurveysOutputModel>>
    {
		public IEnumerable<EmployeeInputModel> Employees{ get; set; }

		public class CreateEmployeeSurveysCommandHandler : IRequestHandler<CreateEmployeesSurveysCommand, Result<CreateEmployeesSurveysOutputModel>>
        {
            private readonly IEmployeeSurveyFactory _employeeSurveyFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;
            private readonly IEmployeeSurveyDomainRepository _userSurveyDomainRepository;

            public CreateEmployeeSurveysCommandHandler(
                IEmployeeSurveyFactory employeeSurveyFactory,
                ISurveyDomainRepository surveyDomainRepository,
                IEmployeeSurveyDomainRepository employeeSurveyDomainRepository)
            {
                this._employeeSurveyFactory = employeeSurveyFactory;
                this._surveyDomainRepository = surveyDomainRepository;
                this._userSurveyDomainRepository = employeeSurveyDomainRepository;
            }

            public async Task<Result<CreateEmployeesSurveysOutputModel>> Handle(
                CreateEmployeesSurveysCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyDomainRepository.GetById(request.Id);

                var employeeSurveys = new List<Domain.Models.EmployeeSurvey>();
                var messages = new List<EmployeeSurveyCreatedMessage>();

                foreach (var employee in request.Employees)
                {
                    var employeeSurvey = this._employeeSurveyFactory
                    .WithSurvey(survey)
                    .WithEmployeeId(employee.EmployeeId)
                    .Build();

                    employeeSurveys.Add(employeeSurvey);

                    messages.Add(new EmployeeSurveyCreatedMessage(employee.UserId, survey.Name));
                }

                await this._userSurveyDomainRepository.Save(employeeSurveys, cancellationToken, messages.ToArray());

                return new CreateEmployeesSurveysOutputModel(survey.Id);
            }
        }
    }
}
