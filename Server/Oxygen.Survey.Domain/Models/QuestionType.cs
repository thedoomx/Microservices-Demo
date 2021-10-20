namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using static Oxygen.Survey.Domain.Models.ModelConstants.QuestionType;

    public class QuestionType : Entity<int>
    {
        internal QuestionType(string type)
        {
            this.Validate(type);
        }

        public string Type { get; private set; }

        public QuestionType ChangeType(string type)
        {
            this.ValidateType(type);
            this.Type = type;

            return this;
        }

        private void Validate(string type)
        {
            this.ValidateType(type);
        }

        private void ValidateType(string type)
            => Guard.ForStringLength<InvalidQuestionTypeException>(
                type,
                MinTypeLength,
                MaxTypeLength,
                nameof(this.Type));
    }
}
