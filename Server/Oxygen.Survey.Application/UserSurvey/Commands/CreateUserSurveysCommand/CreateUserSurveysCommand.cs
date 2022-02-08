namespace Oxygen.Survey.Application.UserSurvey.Commands.CreateUserSurveysCommand
{
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using Oxygen.Survey.Application.UserSurvey.Commands.Common;
	using Oxygen.Infrastructure.Common.Messages.Survey;
	using System.Collections.Generic;

	public class CreateUserSurveysCommand : UserSurveysCommand<CreateUserSurveysCommand>, IRequest<Result<CreateUserSurveysOutputModel>>
    {
        public int SurveyId { get; set; }

        public class CreateUserSurveysCommandHandler : IRequestHandler<CreateUserSurveysCommand, Result<CreateUserSurveysOutputModel>>
        {
            private readonly IUserSurveyFactory _userSurveyFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;
            private readonly IUserSurveyDomainRepository _userSurveyDomainRepository;

            public CreateUserSurveysCommandHandler(
                IUserSurveyFactory userSurveyFactory,
                ISurveyDomainRepository surveyDomainRepository,
                IUserSurveyDomainRepository userSurveyDomainRepository)
            {
                this._userSurveyFactory = userSurveyFactory;
                this._surveyDomainRepository = surveyDomainRepository;
                this._userSurveyDomainRepository = userSurveyDomainRepository;
            }

            public async Task<Result<CreateUserSurveysOutputModel>> Handle(
                CreateUserSurveysCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyDomainRepository.GetById(request.SurveyId);

                var userSurveys = new List<Domain.Models.UserSurvey>();
                var messages = new List<UserSurveyCreatedMessage>();

                foreach (var user in request.UserSurveys)
                {
                    var userSurvey = this._userSurveyFactory
                    .WithSurvey(survey)
                    .WithUserId(user.UserId)
                    .Build();

                    userSurveys.Add(userSurvey);

                    messages.Add(new UserSurveyCreatedMessage(user.UserId, survey.Name));
                }

                await this._userSurveyDomainRepository.Save(userSurveys, cancellationToken, messages.ToArray());

                return new CreateUserSurveysOutputModel(survey.Id);
            }
        }
    }
}
