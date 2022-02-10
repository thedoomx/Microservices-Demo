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

    public class CreateEmployeeSurveyItemsCommand : EmployeeSurveyItemsCommand<CreateEmployeeSurveyItemsCommand>, IRequest<Result<CreateEmployeeSurveyItemsOutputModel>>
    {
        public class CreateEmployeeSurveyItemsCommandHandler : IRequestHandler<CreateEmployeeSurveyItemsCommand, Result<CreateEmployeeSurveyItemsOutputModel>>
        {
            private readonly IEmployeeSurveyItemFactory _employeeSurveyItemFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;
            private readonly IEmployeeSurveyDomainRepository _employeeSurveyDomainRepository;

            public CreateEmployeeSurveyItemsCommandHandler(
                IEmployeeSurveyItemFactory employeeSurveyItemFactory,
                ISurveyDomainRepository surveyDomainRepository,
                IEmployeeSurveyDomainRepository employeeSurveyDomainRepository)
            {
                this._employeeSurveyItemFactory = employeeSurveyItemFactory;
                this._surveyDomainRepository = surveyDomainRepository;
                this._employeeSurveyDomainRepository = employeeSurveyDomainRepository;
            }

            public async Task<Result<CreateEmployeeSurveyItemsOutputModel>> Handle(
                CreateEmployeeSurveyItemsCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyDomainRepository.GetSurveyWithQuestionsDataById(request.SurveyId);

                var employeeSurvey = await this._employeeSurveyDomainRepository.GetById(request.Id);

                foreach (var questionAnswer in request.QuestionAnswers)
                {
                    var question = survey.Questions.FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                    var questionItem = question.QuestionItems.FirstOrDefault(x => x.Id == questionAnswer.QuestionItemId);

                    var employeeSurveyItem = this._employeeSurveyItemFactory
                    .WithQuestion(question)
                    .WithQuestionItem(questionItem)
                    .Build();

                    employeeSurvey.AddEmployeeSurveyItem(employeeSurveyItem);
                }

                await this._employeeSurveyDomainRepository.Save(employeeSurvey);

                return new CreateEmployeeSurveyItemsOutputModel(employeeSurvey.Id);
            }
        }
    }
}
