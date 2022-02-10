namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveysCommand
{
    public class CreateEmployeesSurveysOutputModel
    {
        public CreateEmployeesSurveysOutputModel(int surveyId)
            => this.SurveyId = surveyId;

        public int SurveyId { get; }
    }
}
