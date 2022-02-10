namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.CreateUserSurveyCommand
{
    public class CreateEmployeeSurveyItemsOutputModel
    {
        public CreateEmployeeSurveyItemsOutputModel(int employeeSurveyId)
            => this.EmployeeSurveyId = employeeSurveyId;

        public int EmployeeSurveyId { get; }
    }
}
