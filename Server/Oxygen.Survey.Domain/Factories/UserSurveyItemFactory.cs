namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    internal class UserSurveyItemFactory : IUserSurveyItemFactory
    {
        private Question question = default!;
        private bool questionSet = false;

        private QuestionItem questionItem = default!;
        private bool questionItemSet = default!;

        public IUserSurveyItemFactory WithQuestion(Question question)
        {
            this.question = question;
            this.questionSet = true;

            return this;
        }

        public IUserSurveyItemFactory WithQuestionItem(QuestionItem questionItem)
        {
            this.questionItem = questionItem;
            this.questionItemSet = true;

            return this;
        }

        public UserSurveyItem Build()
        {
            if (!this.questionSet)
            {
                throw new InvalidUserSurveyItemException("Question must have a value.");
            }

            if (!this.questionItemSet)
            {
                throw new InvalidUserSurveyItemException("Question item must have a value.");
            }

            return new UserSurveyItem(this.question, this.questionItem);
        }
    }
}
