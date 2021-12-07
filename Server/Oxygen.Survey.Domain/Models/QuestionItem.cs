namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using static Oxygen.Survey.Domain.Models.ModelConstants.QuestionItem;

    public class QuestionItem : Entity<int>
    {
        public QuestionItem(string description)
        {
            this.Validate(description);

            this.Description = description;
        }

        public string Description { get; private set; }

        public QuestionItem ChangeDescription(string description)
        {
            this.ValidateType(description);
            this.Description = description;

            return this;
        }

        private void Validate(string description)
        {
            this.ValidateType(description);
        }

        private void ValidateType(string description)
            => Guard.ForStringLength<InvalidQuestionItemException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
    }
}
