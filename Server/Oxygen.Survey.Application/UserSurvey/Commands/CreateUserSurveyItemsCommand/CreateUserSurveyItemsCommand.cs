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

    public class CreateUserSurveyItemsCommand : UserSurveyItemsCommand<CreateUserSurveyItemsCommand>, IRequest<Result<CreateUserSurveyItemsOutputModel>>
    {
        public class CreateUserSurveyItemsCommandHandler : IRequestHandler<CreateUserSurveyItemsCommand, Result<CreateUserSurveyItemsOutputModel>>
        {
            private readonly IUserSurveyItemFactory _userSurveyItemFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;
            private readonly IUserSurveyDomainRepository _userSurveyDomainRepository;

            public CreateUserSurveyItemsCommandHandler(
                IUserSurveyItemFactory userSurveyItemFactory,
                ISurveyDomainRepository surveyDomainRepository,
                IUserSurveyDomainRepository userSurveyDomainRepository)
            {
                this._userSurveyItemFactory = userSurveyItemFactory;
                this._surveyDomainRepository = surveyDomainRepository;
                this._userSurveyDomainRepository = userSurveyDomainRepository;
            }

            public async Task<Result<CreateUserSurveyItemsOutputModel>> Handle(
                CreateUserSurveyItemsCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyDomainRepository.GetSurveyWithQuestionsDataById(request.SurveyId);

                var userSurvey = await this._userSurveyDomainRepository.GetById(request.Id);

                foreach (var questionAnswer in request.QuestionAnswers)
                {
                    var question = survey.Questions.FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                    var questionItem = question.QuestionItems.FirstOrDefault(x => x.Id == questionAnswer.QuestionItemId);

                    var userSurveyItem = this._userSurveyItemFactory
                    .WithQuestion(question)
                    .WithQuestionItem(questionItem)
                    .Build();

                    userSurvey.AddUserSurveyItem(userSurveyItem);
                }

                await this._userSurveyDomainRepository.Save(userSurvey);

                return new CreateUserSurveyItemsOutputModel(userSurvey.Id);
            }
        }
    }
}
