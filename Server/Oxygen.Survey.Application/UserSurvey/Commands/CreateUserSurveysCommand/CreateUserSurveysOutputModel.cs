namespace Oxygen.Survey.Application.UserSurvey.Commands.CreateUserSurveysCommand
{
    public class CreateUserSurveysOutputModel
    {
        public CreateUserSurveysOutputModel(int surveyId)
            => this.SurveyId = surveyId;

        public int SurveyId { get; }
    }
}
