namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    public class QuestionFactory
    {
        private QuestionType questionType = default!;
        private bool questionTypeSet = false;

        private string questionDescription = default!;
        private bool questionIsRequired = default!;

        public QuestionFactory WithDescription(string description)
        {
            this.questionDescription = description;
            return this;
        }

        public QuestionFactory WithRequired(bool isRequired)
        {
            this.questionIsRequired = isRequired;
            return this;
        }

        public QuestionFactory WithQuestionType(string type)
            => this.WithQuestionType(new QuestionType(type));

        public QuestionFactory WithQuestionType(QuestionType questionType)
        {
            this.questionType = questionType;
            this.questionTypeSet = true;
            return this;
        }

        public Question Build()
        {
            if (!this.questionTypeSet)
            {
                throw new InvalidQuestionException("Question type must have a value.");
            }

            return new Question(
                this.questionDescription,
                this.questionIsRequired,
                this.questionType);
        }
    }
}
