namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveyCommand
{
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
    using Oxygen.Survey.Application.EmployeeSurvey.Commands.Common;

    public class CreateEmployeeSurveyAnswersCommand : EmployeeSurveyAnswersCommand<CreateEmployeeSurveyAnswersCommand>, IRequest<Result<CreateEmployeeSurveyAnswersOutputModel>>
    {
        public class CreateEmployeeSurveyAnswersCommandHandler : IRequestHandler<CreateEmployeeSurveyAnswersCommand, Result<CreateEmployeeSurveyAnswersOutputModel>>
        {
            private readonly IEmployeeSurveyAnswerFactory _employeeSurveyAnswerFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;
            private readonly IEmployeeSurveyDomainRepository _employeeSurveyDomainRepository;

            public CreateEmployeeSurveyAnswersCommandHandler(
                IEmployeeSurveyAnswerFactory employeeSurveyAnswerFactory,
                ISurveyDomainRepository surveyDomainRepository,
                IEmployeeSurveyDomainRepository employeeSurveyDomainRepository)
            {
                this._employeeSurveyAnswerFactory = employeeSurveyAnswerFactory;
                this._surveyDomainRepository = surveyDomainRepository;
                this._employeeSurveyDomainRepository = employeeSurveyDomainRepository;
            }

            public async Task<Result<CreateEmployeeSurveyAnswersOutputModel>> Handle(
                CreateEmployeeSurveyAnswersCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyDomainRepository.GetSurveyWithQuestionsDataById(request.SurveyId);

                var employeeSurvey = await this._employeeSurveyDomainRepository.GetById(request.Id);

                foreach (var questionAnswer in request.QuestionAnswers)
                {
                    var question = survey.Questions.FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                    var questionAnswerEntity = question.QuestionAnswers.FirstOrDefault(x => x.Id == questionAnswer.QuestionAnswerId);

                    var employeeSurveyAnswer = this._employeeSurveyAnswerFactory
                    .WithQuestion(question)
                    .WithQuestionAnswer(questionAnswerEntity)
                    .Build();

                    employeeSurvey.AddEmployeeSurveyAnswer(employeeSurveyAnswer);
                }

                await this._employeeSurveyDomainRepository.Save(employeeSurvey);

                return new CreateEmployeeSurveyAnswersOutputModel(employeeSurvey.Id);
            }
        }
    }
}
