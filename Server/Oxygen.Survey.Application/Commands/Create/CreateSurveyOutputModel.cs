namespace Oxygen.Survey.Application.Commands.Create
{
    public class CreateSurveyOutputModel
    {
        public CreateSurveyOutputModel(int surveyId)
            => this.SurveyId = surveyId;

        public int SurveyId { get; }
    }
}
