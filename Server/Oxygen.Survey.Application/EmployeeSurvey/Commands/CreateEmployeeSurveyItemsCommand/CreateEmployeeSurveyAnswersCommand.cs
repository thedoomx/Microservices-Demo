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
	using static Oxygen.Common.Constants.GlobalConstants;

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

				var employeeSurvey = await this._employeeSurveyDomainRepository.GetByEmployeeIdAndSurveyId(request.EmployeeId, request.SurveyId);

				foreach (var questionAnswer in request.QuestionAnswers)
				{
					var question = survey.Questions.FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
					var employeeSurveyAnswerFactory = this._employeeSurveyAnswerFactory
					.WithQuestion(question);

					switch (question.QuestionType.Type)
					{
						case (QuestionType.Checkbox):
							employeeSurveyAnswerFactory.WithBoolValue(questionAnswer.BoolValue);
							break;
						case (QuestionType.Free_text):
							employeeSurveyAnswerFactory.WithTextValue(questionAnswer.TextValue);
							break;
						case (QuestionType.Radio):
							var questionAnswerEntity = 
								question.QuestionAnswers.FirstOrDefault(x => x.Id == questionAnswer.QuestionAnswerId);
							employeeSurveyAnswerFactory.WithQuestionAnswer(questionAnswerEntity);
							break;
						default:
							break;
					}

					var employeeSurveyAnswer = employeeSurveyAnswerFactory.Build();

					await this._employeeSurveyDomainRepository.AddEmployeeSurveyAnswer(employeeSurveyAnswer);

					employeeSurvey.AddEmployeeSurveyAnswer(employeeSurveyAnswer);
				}

				employeeSurvey.Submit();

				await this._employeeSurveyDomainRepository.Save(employeeSurvey);

				return new CreateEmployeeSurveyAnswersOutputModel(employeeSurvey.Id);
			}
		}
	}
}
