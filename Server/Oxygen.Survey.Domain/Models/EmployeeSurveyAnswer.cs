namespace Oxygen.Survey.Domain.Models
{
	using Oxygen.Common.Constants;
	using Oxygen.Domain.Common.Models;
	using Oxygen.Survey.Domain.Exceptions;
	using static Oxygen.Survey.Domain.Models.ModelConstants.EmployeeSurveyAnswer;

	public class EmployeeSurveyAnswer : Entity<int>
	{
		internal EmployeeSurveyAnswer(Question question, QuestionAnswer questionAnswer, string textValue, bool? boolValue)
		{
			this.Validate(question, questionAnswer, textValue, boolValue);

			this.Question = question;
			this.QuestionAnswer = questionAnswer;

			this.TextValue = textValue;
			this.BoolValue = boolValue;
		}

		private EmployeeSurveyAnswer(string textValue, bool? boolValue)
		{
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

		private void Validate(Question question, QuestionAnswer questionAnswer, string textValue, bool? boolValue)
		{
			this.ValidateQuestionAnswer(question, questionAnswer);
			this.ValidateTextValue(question, textValue);
			this.ValidateBoolValue(question, boolValue);
		}

		private void ValidateQuestionAnswer(Question question, QuestionAnswer questionAnswer)
		{
			if (question.QuestionType.Type != GlobalConstants.QuestionType.Radio)
			{
				return;
			}

			Guard.Against<InvalidEmployeeSurveyAnswerException>(
				questionAnswer,
				null,
				nameof(this.QuestionAnswer));
		}

		private void ValidateTextValue(Question question, string textValue)
		{
			if (question.QuestionType.Type != GlobalConstants.QuestionType.Free_text)
			{
				return;
			}

			Guard.ForStringLength<InvalidEmployeeSurveyAnswerException>(
				textValue,
				MinTextValueLength,
				MaxTextValueLength,
				nameof(this.TextValue));
		}

		private void ValidateBoolValue(Question question, bool? boolValue)
		{
			if (question.QuestionType.Type != GlobalConstants.QuestionType.Checkbox)
			{
				return;
			}

			Guard.AgainstEmptyBool<InvalidEmployeeSurveyAnswerException>(
				boolValue,
				nameof(this.BoolValue));
		}
	}
}
