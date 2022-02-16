namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
	using Oxygen.Survey.Domain.Exceptions;
    using static Oxygen.Survey.Domain.Models.ModelConstants.EmployeeSurveyAnswer;

    public class EmployeeSurveyAnswer : Entity<int>
    {
        internal EmployeeSurveyAnswer(Question question, QuestionAnswer questionAnswer, string freeText)
        {
            this.Validate(freeText);

            this.Question = question;
            this.QuestionAnswer = questionAnswer;
            this.FreeText = freeText;
        }

        private EmployeeSurveyAnswer(string freeText)
        {
            this.Validate(freeText);

            this.FreeText = freeText;
        }

        public Question Question { get; private set; }

        public QuestionAnswer QuestionAnswer { get; private set; }

		public string FreeText { get; private set; }

		public EmployeeSurveyAnswer ChangeQuestion(Question question)
        {
            this.Question = question;

            return this;
        }

        public EmployeeSurveyAnswer ChangeQuestionAnswer(QuestionAnswer questionAnswer)
        {
            this.QuestionAnswer = questionAnswer;

            return this;
        }

        private void Validate(string freeText)
        {
            this.ValidateFreeText(freeText);
        }

        private void ValidateFreeText(string description)
            => Guard.ForStringLength<InvalidEmployeeSurveyAnswerException>(
                description,
                MinFreeTextLength,
                MaxFreeTextLength,
                nameof(this.FreeText));
    }
}
