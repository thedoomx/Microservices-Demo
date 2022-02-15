namespace Oxygen.Survey.Application.Survey.Commands.Create
{
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Application.Survey.Commands.Common;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
	using Oxygen.Survey.Domain.Models;

	public class CreateSurveyCommand : SurveyCommand<CreateSurveyCommand>, IRequest<Result<CreateSurveyOutputModel>>
    {
        public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, Result<CreateSurveyOutputModel>>
        {
            private readonly ISurveyFactory _surveyFactory;
            private readonly IQuestionItemFactory _questionItemFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;

            public CreateSurveyCommandHandler(
                ISurveyFactory surveyFactory,
                IQuestionItemFactory _questionItemFactory,
                ISurveyDomainRepository surveyDomainRepository)
            {
                this._surveyFactory = surveyFactory;
                this._surveyDomainRepository = surveyDomainRepository;
            }

            public async Task<Result<CreateSurveyOutputModel>> Handle(
                CreateSurveyCommand request,
                CancellationToken cancellationToken)
            {
                var surveyType = await this._surveyDomainRepository.GetSurveyTypeById(request.SurveyType);

                var questionTypes = await this._surveyDomainRepository.GetQuestionTypes();

                var surveyFactory = this._surveyFactory
                    .WithName(request.Name)
                    .WithSummary(request.Summary)
                    .WithSurveyType(surveyType);

                foreach (var question in request.Questions)
                {
                    var tempList = new List<QuestionItem>();
					foreach (var questionItem in question.QuestionItems)
					{
                        tempList.Add(this._questionItemFactory.WithDescription(questionItem.Description).Build());
					}

                    var questionType = questionTypes.FirstOrDefault(x => x.Id == question.QuestionType);

                    surveyFactory = surveyFactory.WithQuestion(x => x
                    .WithDescription(question.Description)
                    .WithRequired(question.IsRequired)
                    .WithQuestionType(questionType)
                    .WithQuestionItems(tempList));
                }

                var survey = surveyFactory.Build();

                await this._surveyDomainRepository.Save(survey);

                return new CreateSurveyOutputModel(survey.Id);
            }
        }
    }
}
