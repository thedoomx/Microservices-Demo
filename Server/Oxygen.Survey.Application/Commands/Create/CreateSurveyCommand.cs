
namespace Oxygen.Survey.Application.Commands.Create
{
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Survey.Application.Commands.Common;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class CreateSurveyCommand : SurveyCommand<CreateSurveyCommand>, IRequest<Result<CreateSurveyOutputModel>>
    {
        public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, Result<CreateSurveyOutputModel>>
        {
            private readonly ISurveyFactory _surveyFactory;
            private readonly ISurveyDomainRepository _surveyDomainRepository;

            public CreateSurveyCommandHandler(
                ISurveyFactory surveyFactory,
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
                    var questionType = questionTypes.FirstOrDefault(x => x.Id == question.QuestionType);

                    surveyFactory = surveyFactory.WithQuestion(x => x
                    .WithDescription(question.Description)
                    .WithRequired(question.IsRequired)
                    .WithQuestionType(questionType));
                }

                var survey = surveyFactory.Build();

                await this._surveyDomainRepository.Save(survey);

                return new CreateSurveyOutputModel(survey.Id);
            }
        }
    }
}
