﻿namespace Oxygen.Survey.Application.Survey.Commands.Create
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
            private readonly IQuestionFactory _questionFactory;
            private readonly IQuestionItemFactory _questionItemFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;

            public CreateSurveyCommandHandler(
                ISurveyFactory surveyFactory,
                IQuestionFactory questionFactory,
                IQuestionItemFactory questionItemFactory,
                ISurveyDomainRepository surveyDomainRepository)
            {
                this._surveyFactory = surveyFactory;
                this._questionFactory = questionFactory;
                this._questionItemFactory = questionItemFactory;
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
                    var questionType = questionTypes.FirstOrDefault(x => x.Id == question.QuestionType);

                    

                    var questionItemsList = new List<QuestionItem>();
					foreach (var questionItem in question.QuestionItems)
					{
                        var questionItemEntity = this._questionItemFactory
                                                    .WithDescription(questionItem.Description)
                                                    .Build();

                        await this._surveyDomainRepository.AddQuestionItem(questionItemEntity, cancellationToken);

                        questionItemsList.Add(questionItemEntity);
					}

                    var questionEntity = this._questionFactory
                                            .WithDescription(question.Description)
                                            .WithRequired(question.IsRequired)
                                            .WithQuestionType(questionType)
                                            .WithQuestionItems(questionItemsList)
                                            .Build();

                    await this._surveyDomainRepository.AddQuestion(questionEntity, cancellationToken);

                    surveyFactory = surveyFactory.WithQuestion(questionEntity);
                }

                var survey = surveyFactory.Build();

                await this._surveyDomainRepository.Save(survey);

                return new CreateSurveyOutputModel(survey.Id);
            }
        }
    }
}
