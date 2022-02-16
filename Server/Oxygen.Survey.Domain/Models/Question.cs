namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using System.Collections.Generic;
    using System.Linq;
    using static Oxygen.Survey.Domain.Models.ModelConstants.Question;

    public class Question : Entity<int>
    {
        private readonly HashSet<QuestionAnswer> questionAnswers;

        internal Question(string description, bool isRequired, QuestionType questionType)
        {
            this.Validate(description);

            this.Description = description;
            this.IsRequired = isRequired;
            this.QuestionType = questionType;

            this.questionAnswers = new HashSet<QuestionAnswer>();
        }

        private Question(string description, bool isRequired)
        {
            this.Validate(description);

            this.Description = description;
            this.IsRequired = isRequired;

            this.questionAnswers = new HashSet<QuestionAnswer>();
        }

        public string Description { get; private set; }

        public bool IsRequired { get; private set; }

        public QuestionType QuestionType { get; private set; }

        public Question ChangeDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        public Question ChangeRequired()
        {
            this.IsRequired = !this.IsRequired;

            return this;
        }

        public Question ChangeQuestionType(QuestionType questionType)
        {
            this.QuestionType = questionType;

            return this;
        }

        public IReadOnlyCollection<QuestionAnswer> QuestionAnswers => this.questionAnswers.ToList().AsReadOnly();

        public void AddQuestionAnswer(QuestionAnswer questionAnswer)
        {
            this.questionAnswers.Add(questionAnswer);
        }

        private void Validate(string description)
        {
            this.ValidateDescription(description);
        }

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidQuestionException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
    }
}
