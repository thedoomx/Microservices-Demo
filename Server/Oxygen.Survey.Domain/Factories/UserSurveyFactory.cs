namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Models;
    using System;
    using System.Collections.Generic;

    internal class UserSurveyFactory : IUserSurveyFactory
    {
        private string userId = default!;
        private int surveyId = default!;
        private bool isSubmitted = default!;

        private readonly List<UserSurveyItem> userSurveyItems;

        internal UserSurveyFactory()
        {
            this.userSurveyItems = new List<UserSurveyItem>();
        }

        public IUserSurveyFactory WithUserId(string userId)
        {
            this.userId = userId;
            return this;
        }

        public IUserSurveyFactory WithSurveyId(int surveyId)
        {
            this.surveyId = surveyId;
            return this;
        }

        public IUserSurveyFactory WithQuestionAnswer(Action<UserSurveyItemFactory> userSurveyItem)
        {
            var questionFactory = new UserSurveyItemFactory();

            userSurveyItem(questionFactory);

            this.userSurveyItems.Add(questionFactory.Build());

            return this;
        }

        public UserSurvey Build()
        {
            var userSurvey = new UserSurvey(
                this.userId,
                this.surveyId,
                this.isSubmitted);

            this.userSurveyItems.ForEach(x => userSurvey.AddUserSurveyItem(x));

            return userSurvey;
        }
    }
}
