namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
	using Oxygen.Survey.Domain.Exceptions;
    using static Oxygen.Survey.Domain.Models.ModelConstants.EmployeeSurveyAnswer;

    public class EmployeeSurveyAnswer : Entity<int>
    {
        internal EmployeeSurveyAnswer(Question question, QuestionAnswer questionAnswer, string textValue, bool? boolValue)
        {
            this.Validate(textValue);

            this.Question = question;
            this.QuestionAnswer = questionAnswer;

            this.TextValue = textValue;
            this.BoolValue = boolValue;
        }

        private EmployeeSurveyAnswer(string textValue, bool? boolValue)
        {
            this.Validate(textValue);

            this.TextValue = textValue;
            this.BoolValue = boolValue;
        }

        public Question Question { get; private set; }

        public QuestionAnswer QuestionAnswer { get; private set; }

		public string TextValue { get; private set; }

        public bool? BoolValue { get; private set; }

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

        private void Validate(string textValue)
        {
            this.ValidateFreeText(textValue);
        }

        private void ValidateFreeText(string textValue)
            => Guard.ForStringLength<InvalidEmployeeSurveyAnswerException>(
                textValue,
                MinTextValueLength,
                MaxTextValueLength,
                nameof(this.TextValue));
    }
}
