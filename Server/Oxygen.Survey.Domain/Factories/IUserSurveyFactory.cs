namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System;

    public interface IUserSurveyFactory : IFactory<UserSurvey>
    {
        IUserSurveyFactory WithUserId(string userId);

        IUserSurveyFactory WithSurveyId(int surveyId);

        IUserSurveyFactory WithQuestionAnswer(Action<UserSurveyItemFactory> userSurveyItem);
    }
}
