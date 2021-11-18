namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;

    public interface IUserSurveyItemFactory : IBuild<UserSurveyItem>
    {
        UserSurveyItemFactory WithQuestion(Question question);

        UserSurveyItemFactory WithQuestionItem(QuestionItem questionItem);
    }
}
