namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    public class QuestionItemFactory : IQuestionItemFactory
    {
        private string questionItemDescription = default!;

        public IQuestionItemFactory WithDescription(string description)
        {
            this.questionItemDescription = description;
            return this;
        }

        public QuestionItem Build()
        {
            return new QuestionItem(
                this.questionItemDescription);
        }
    }
}
