namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Models;

    internal class UserSurveyFactory : IUserSurveyFactory
    {
        private string userId = default!;
        private int surveyId = default!;
        private bool isSubmitted = default!;

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

        public UserSurvey Build()
        {
            var userSurvey = new UserSurvey(
                this.userId,
                this.surveyId,
                this.isSubmitted);

            return userSurvey;
        }
    }
}
