namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;
    using System;
    using System.Collections.Generic;

    internal class UserSurveyFactory : IUserSurveyFactory
    {
        private string userId = default!;
        private Survey survey = default!;
        private bool surveySet = false;
        private bool isSubmitted = default!;

        private List<UserSurveyItem> userSurveyItems = new List<UserSurveyItem>();

        public IUserSurveyFactory WithUserId(string userId)
        {
            this.userId = userId;
            return this;
        }

        public IUserSurveyFactory WithSurvey(Survey survey)
        {
            this.survey = survey;
            this.surveySet = true;
            return this;
        }

        public IUserSurveyFactory WithQuestionAnswer(Action<IUserSurveyItemFactory> userSurveyItem)
        {
            var questionFactory = new UserSurveyItemFactory();

            userSurveyItem(questionFactory);

            this.userSurveyItems.Add(questionFactory.Build());

            return this;
        }

        public UserSurvey Build()
        {
            if (!this.surveySet)
            {
                throw new InvalidUserSurveyException("Survey must have a value.");
            }

            var userSurvey = new UserSurvey(
                this.userId,
                this.survey,
                this.isSubmitted);

            this.userSurveyItems.ForEach(x => userSurvey.AddUserSurveyItem(x));

            return userSurvey;
        }
    }
}
