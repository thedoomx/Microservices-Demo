namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;

    public class EmployeeSurveyItem : Entity<int>
    {
        internal EmployeeSurveyItem(Question question, QuestionItem questionItem)
        {
            this.Question = question;
            this.QuestionItem = questionItem;
        }

        private EmployeeSurveyItem()
        {
        }

        public Question Question { get; private set; }

        public QuestionItem QuestionItem { get; private set; }

        public EmployeeSurveyItem ChangeQuestion(Question question)
        {
            this.Question = question;

            return this;
        }

        public EmployeeSurveyItem ChangeQuestionItem(QuestionItem questionItem)
        {
            this.QuestionItem = questionItem;

            return this;
        }
    }
}
