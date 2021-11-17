namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System;

    public interface ISurveyFactory : IFactory<Survey>
    {
        ISurveyFactory WithName(string name);

        ISurveyFactory WithSummary(string summary);

        ISurveyFactory WithSurveyType(SurveyType surveyType);

        ISurveyFactory WithQuestion(Action<QuestionFactory> question);
    }
}
