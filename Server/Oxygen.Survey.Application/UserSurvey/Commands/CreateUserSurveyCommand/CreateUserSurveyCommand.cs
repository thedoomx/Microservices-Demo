namespace Oxygen.Survey.Application.UserSurvey.Commands.CreateUserSurveyCommand
{
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
    using Oxygen.Survey.Application.UserSurvey.Commands.Common;

    public class CreateUserSurveyCommand : UserSurveyCommand<CreateUserSurveyCommand>, IRequest<Result<CreateUserSurveyOutputModel>>
    {
        public class CreateUserSurveyCommandHandler : IRequestHandler<CreateUserSurveyCommand, Result<CreateUserSurveyOutputModel>>
        {
            private readonly IUserSurveyFactory _userSurveyFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;
            private readonly IUserSurveyDomainRepository _userSurveyDomainRepository;

            public CreateUserSurveyCommandHandler(
                IUserSurveyFactory userSurveyFactory,
                ISurveyDomainRepository surveyDomainRepository,
                IUserSurveyDomainRepository userSurveyDomainRepository)
            {
                this._userSurveyFactory = userSurveyFactory;
                this._surveyDomainRepository = surveyDomainRepository;
                this._userSurveyDomainRepository = userSurveyDomainRepository;
            }

            public async Task<Result<CreateUserSurveyOutputModel>> Handle(
                CreateUserSurveyCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyDomainRepository.GetSurveyWithQuestionsDataById(request.SurveyId);

                var userSurveyFactory = this._userSurveyFactory
                    .WithSurveyId(survey.Id)
                    .WithUserId(request.UserId);

                foreach (var questionAnswer in request.QuestionAnswers)
                {
                    var question = survey.Questions.FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                    var questionItem = question.QuestionItems.FirstOrDefault(x => x.Id == questionAnswer.QuestionItemId);

                    userSurveyFactory = userSurveyFactory.WithQuestionAnswer(x => x
                    .WithQuestion(question)
                    .WithQuestionItem(questionItem));
                }

                var userSurvey = userSurveyFactory.Build();

                await this._userSurveyDomainRepository.Save(userSurvey);

                return new CreateUserSurveyOutputModel(userSurvey.Id);
            }
        }
    }
}
