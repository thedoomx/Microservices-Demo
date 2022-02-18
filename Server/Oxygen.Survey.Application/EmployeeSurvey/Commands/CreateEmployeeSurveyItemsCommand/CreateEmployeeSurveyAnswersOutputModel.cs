namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveyCommand
{
    public class CreateEmployeeSurveyAnswersOutputModel
    {
        public CreateEmployeeSurveyAnswersOutputModel(int employeeSurveyId)
            => this.EmployeeSurveyId = employeeSurveyId;

        public int EmployeeSurveyId { get; }
    }
}
