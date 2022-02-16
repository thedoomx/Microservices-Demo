namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System;

    public interface IEmployeeSurveyFactory : IFactory<EmployeeSurvey>
    {
        IEmployeeSurveyFactory WithEmployeeId(int employeeId);

        IEmployeeSurveyFactory WithSurvey(Survey survey);

        IEmployeeSurveyFactory WithQuestionAnswer(Action<IEmployeeSurveyAnswerFactory> employeeSurveyAnswer);
    }
}
