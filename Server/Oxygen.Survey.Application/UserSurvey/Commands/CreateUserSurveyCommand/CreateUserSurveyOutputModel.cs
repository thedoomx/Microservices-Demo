namespace Oxygen.Survey.Application.UserSurvey.Commands.CreateUserSurveyCommand
{
    public class CreateUserSurveyOutputModel
    {
        public CreateUserSurveyOutputModel(int userSurveyId)
            => this.UserSurveyId = userSurveyId;

        public int UserSurveyId { get; }
    }
}
