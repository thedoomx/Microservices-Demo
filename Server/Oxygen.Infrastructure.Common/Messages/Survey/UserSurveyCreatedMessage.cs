namespace Oxygen.Infrastructure.Common.Messages.Survey
{
    public class UserSurveyCreatedMessage
    {
        public UserSurveyCreatedMessage(string userId, string surveyName)
        {
            this.UserId = userId;
            this.SurveyName = surveyName;
        }

        public string UserId { get; set; }

        public string SurveyName { get; set; }
    }
}
