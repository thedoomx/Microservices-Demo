namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Common.Constants;
	using Oxygen.Survey.Domain.Exceptions;
	using Oxygen.Survey.Domain.Models;
	using System;
	using System.Collections.Generic;

	public class QuestionFactory : IQuestionFactory
	{
		private QuestionType questionType = default!;
		private bool questionTypeSet = false;

		private string questionDescription = default!;
		private bool questionIsRequired = default!;

		private List<QuestionAnswer> questionAnswers = new List<QuestionAnswer>();

		public IQuestionFactory WithDescription(string description)
		{
			this.questionDescription = description;
			return this;
		}

		public IQuestionFactory WithRequired(bool isRequired)
		{
			this.questionIsRequired = isRequired;
			return this;
		}

		public IQuestionFactory WithQuestionType(string type)
			=> this.WithQuestionType(new QuestionType(type));

		public IQuestionFactory WithQuestionType(QuestionType questionType)
		{
			this.questionType = questionType;
			this.questionTypeSet = true;
			return this;
		}

		public IQuestionFactory WithQuestionAnswer(QuestionAnswer questionAnswer)
		{
			this.questionAnswers.Add(questionAnswer);

			return this;
		}

		public IQuestionFactory WithQuestionAnswer(Action<IQuestionAnswerFactory> questionAnswer)
		{
			var questionAnswerFactory = new QuestionAnswerFactory();

			questionAnswer(questionAnswerFactory);

			this.questionAnswers.Add(questionAnswerFactory.Build());

			return this;
		}

		public IQuestionFactory WithQuestionAnswers(IEnumerable<QuestionAnswer> questionAnswers)
		{
			this.questionAnswers.AddRange(questionAnswers);

			return this;
		}

		public IQuestionFactory WithQuestionAnswers(IEnumerable<Action<IQuestionAnswerFactory>> questionAnswers)
		{
			foreach (var action in questionAnswers)
			{
				var questionAnswerFactory = new QuestionAnswerFactory();

				action(questionAnswerFactory);

				this.questionAnswers.Add(questionAnswerFactory.Build());
			}

			return this;
		}

		public Question Build()
		{
			if (!this.questionTypeSet)
			{
				throw new InvalidQuestionException("Question type must have a value.");
			}

			if (this.questionAnswers.Count == 0 && this.questionType.Type != GlobalConstants.QuestionType.Free_text)
			{
				throw new InvalidSurveyException("Question must have question answers.");
			}

			var question = new Question(
				this.questionDescription,
				this.questionIsRequired,
				this.questionType);

			this.questionAnswers.ForEach(x => question.AddQuestionAnswer(x));

			this.ResetFields();

			return question;
		}

		private void ResetFields()
		{
			this.questionType = default!;
			this.questionTypeSet = false;

			this.questionDescription = default!;
			this.questionIsRequired = default!;

			this.questionAnswers = new List<QuestionAnswer>();
		}
	}
}
