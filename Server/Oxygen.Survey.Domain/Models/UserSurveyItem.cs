namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;

    public class UserSurveyItem : Entity<int>
    {
        internal UserSurveyItem(Question question, QuestionItem questionItem)
        {
            this.Question = question;
            this.QuestionItem = questionItem;
        }

        private UserSurveyItem()
        {
        }

        public Question Question { get; private set; }

        public QuestionItem QuestionItem { get; private set; }

        public UserSurveyItem ChangeQuestion(Question question)
        {
            this.Question = question;

            return this;
        }

        public UserSurveyItem ChangeQuestionItem(QuestionItem questionItem)
        {
            this.QuestionItem = questionItem;

            return this;
        }
    }
}
