namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using static Oxygen.Survey.Domain.Models.ModelConstants.QuestionAnswer;

    public class QuestionAnswer : Entity<int>
    {
        public QuestionAnswer(string description)
        {
            this.Validate(description);

            this.Description = description;
        }

        public string Description { get; private set; }

        public QuestionAnswer ChangeDescription(string description)
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
            => Guard.ForStringLength<InvalidQuestionAnswerException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
    }
}
