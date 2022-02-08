namespace Oxygen.Survey.Application.UserSurvey.Commands.CreateUserSurveyCommand
{
    public class CreateUserSurveyItemsOutputModel
    {
        public CreateUserSurveyItemsOutputModel(int userSurveyId)
            => this.UserSurveyId = userSurveyId;

        public int UserSurveyId { get; }
    }
}
