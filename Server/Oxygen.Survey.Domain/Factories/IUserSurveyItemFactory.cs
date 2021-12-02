namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;

    public interface IUserSurveyItemFactory : IBuild<UserSurveyItem>
    {
        IUserSurveyItemFactory WithQuestion(Question question);

        IUserSurveyItemFactory WithQuestionItem(QuestionItem questionItem);
    }
}
