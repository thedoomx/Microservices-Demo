namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Common.Constants;
	using Oxygen.Survey.Domain.Exceptions;
	using Oxygen.Survey.Domain.Models;
	using System;

	internal class EmployeeSurveyAnswerFactory : IEmployeeSurveyAnswerFactory
	{
		private Question question = default!;
		private bool questionSet = false;

		private QuestionAnswer questionAnswer = default!;
		private bool questionAnswerSet = default!;

		private string textValue = default!;
		private bool? boolValue = default!;

		public IEmployeeSurveyAnswerFactory WithQuestion(Question question)
		{
			this.question = question;
			this.questionSet = true;

			return this;
		}

		public IEmployeeSurveyAnswerFactory WithQuestionAnswer(QuestionAnswer questionAnswer)
		{
			this.questionAnswer = questionAnswer;
			this.questionAnswerSet = true;

			return this;
		}

		public IEmployeeSurveyAnswerFactory WithTextValue(string textVal)
		{
			this.textValue = textVal;
			return this;
		}

		public IEmployeeSurveyAnswerFactory WithBoolValue(bool? boolVal)
		{
			this.boolValue = boolVal;
			return this;
		}

		public EmployeeSurveyAnswer Build()
		{
			if (!this.questionSet)
			{
				throw new InvalidEmployeeSurveyAnswerException("Question must have a value.");
			}

			if (!this.questionAnswerSet && this.question.QuestionType.Type == GlobalConstants.QuestionType.Radio && this.question.IsRequired)
			{
				throw new InvalidEmployeeSurveyAnswerException("Question answer must have a value.");
			}

			if (!boolValue.HasValue && this.question.QuestionType.Type == GlobalConstants.QuestionType.Checkbox && this.question.IsRequired)
			{
				throw new InvalidEmployeeSurveyAnswerException("Question answer must have a value.");
			}

			if (String.IsNullOrEmpty(textValue) && this.question.QuestionType.Type == GlobalConstants.QuestionType.Free_text && this.question.IsRequired)
			{
				throw new InvalidEmployeeSurveyAnswerException("Question answer must have a value.");
			}

			var employeeSurveyAnswer =
				new EmployeeSurveyAnswer(this.question, this.questionAnswer, this.textValue, this.boolValue);

			this.Reset();

			return employeeSurveyAnswer;
		}

		private void Reset()
		{
			this.question = default!;
			this.questionSet = false;
			this.questionAnswer = default!;
			this.questionAnswerSet = default!;
			this.textValue = default!;
			this.boolValue = default!;
		}
	}
}
