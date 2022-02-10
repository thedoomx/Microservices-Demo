namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;

    public interface IEmployeeSurveyItemFactory : IBuild<EmployeeSurveyItem>
    {
        IEmployeeSurveyItemFactory WithQuestion(Question question);

        IEmployeeSurveyItemFactory WithQuestionItem(QuestionItem questionItem);
    }
}
