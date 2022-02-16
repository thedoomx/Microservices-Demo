namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;

    public interface IEmployeeSurveyAnswerFactory : IBuild<EmployeeSurveyAnswer>
    {
        IEmployeeSurveyAnswerFactory WithQuestion(Question question);

        IEmployeeSurveyAnswerFactory WithQuestionAnswer(QuestionAnswer questionAnswer);
    }
}
