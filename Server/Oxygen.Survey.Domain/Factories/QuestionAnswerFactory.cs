namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    public class QuestionAnswerFactory : IQuestionAnswerFactory
    {
        private string questionAnswerDescription = default!;

        public IQuestionAnswerFactory WithDescription(string description)
        {
            this.questionAnswerDescription = description;
            return this;
        }

        public QuestionAnswer Build()
        {
            return new QuestionAnswer(
                this.questionAnswerDescription);
        }
    }
}
