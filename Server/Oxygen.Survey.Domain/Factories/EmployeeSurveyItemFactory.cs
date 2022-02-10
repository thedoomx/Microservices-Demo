namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    internal class EmployeeSurveyItemFactory : IEmployeeSurveyItemFactory
    {
        private Question question = default!;
        private bool questionSet = false;

        private QuestionItem questionItem = default!;
        private bool questionItemSet = default!;

        public IEmployeeSurveyItemFactory WithQuestion(Question question)
        {
            this.question = question;
            this.questionSet = true;

            return this;
        }

        public IEmployeeSurveyItemFactory WithQuestionItem(QuestionItem questionItem)
        {
            this.questionItem = questionItem;
            this.questionItemSet = true;

            return this;
        }

        public EmployeeSurveyItem Build()
        {
            if (!this.questionSet)
            {
                throw new InvalidEmployeeSurveyItemException("Question must have a value.");
            }

            if (!this.questionItemSet)
            {
                throw new InvalidEmployeeSurveyItemException("Question item must have a value.");
            }

            return new EmployeeSurveyItem(this.question, this.questionItem);
        }
    }
}
