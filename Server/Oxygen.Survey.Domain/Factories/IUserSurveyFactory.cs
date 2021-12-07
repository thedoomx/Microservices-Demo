namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System;

    public interface IUserSurveyFactory : IFactory<UserSurvey>
    {
        IUserSurveyFactory WithUserId(string userId);

        IUserSurveyFactory WithSurvey(Survey survey);

        IUserSurveyFactory WithQuestionAnswer(Action<IUserSurveyItemFactory> userSurveyItem);
    }
}
