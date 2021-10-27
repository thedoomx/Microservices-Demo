namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using static Oxygen.Survey.Domain.Models.ModelConstants.Question;

    public class Question : Entity<int>
    {
        private readonly HashSet<QuestionItem> questionItems;

        internal Question(string description, bool isRequired, QuestionType questionType)
        {
            this.Validate(description);

            this.Description = description;
            this.IsRequired = isRequired;
            this.QuestionType = questionType;

            this.questionItems = new HashSet<QuestionItem>();
        }

        private Question(string description, bool isRequired)
        {
            this.Validate(description);

            this.Description = description;
            this.IsRequired = isRequired;

            this.questionItems = new HashSet<QuestionItem>();
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

        public IReadOnlyCollection<QuestionItem> QuestionItems => this.questionItems.ToList().AsReadOnly();

        public void AddQuestionItem(QuestionItem questionItem)
        {
            this.questionItems.Add(questionItem);

            //this.RaiseEvent(new QuestionItemAddedEvent());
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
