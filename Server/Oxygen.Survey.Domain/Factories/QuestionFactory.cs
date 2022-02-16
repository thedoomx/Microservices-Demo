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

		private List<QuestionItem> questionItems = new List<QuestionItem>();

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

		public IQuestionFactory WithQuestionItem(QuestionItem questionItem)
		{
			this.questionItems.Add(questionItem);

			return this;
		}

		public IQuestionFactory WithQuestionItem(Action<IQuestionItemFactory> questionItem)
		{
			var questionItemFactory = new QuestionItemFactory();

			questionItem(questionItemFactory);

			this.questionItems.Add(questionItemFactory.Build());

			return this;
		}

		public IQuestionFactory WithQuestionItems(IEnumerable<QuestionItem> questionItems)
		{
			this.questionItems.AddRange(questionItems);

			return this;
		}

		public IQuestionFactory WithQuestionItems(IEnumerable<Action<IQuestionItemFactory>> questionItems)
		{
			IList<QuestionItem> tempQuestionItems = new List<QuestionItem>();

			foreach (var action in questionItems)
			{
				var questionItemFactory = new QuestionItemFactory();

				action(questionItemFactory);

				this.questionItems.Add(questionItemFactory.Build());
			}

			return this;
		}

		//public QuestionFactory WithQuestionItems(params Action<QuestionItemFactory>[] questionItems)
		//{
		//    IList<QuestionItem> tempQuestionItems = new List<QuestionItem>();

		//    foreach (var action in questionItems)
		//    {
		//        var questionItemFactory = new QuestionItemFactory();

		//        action(questionItemFactory);

		//        this.questionItems.Add(questionItemFactory.Build());
		//    }

		//    return this;
		//}

		public Question Build()
		{
			if (!this.questionTypeSet)
			{
				throw new InvalidQuestionException("Question type must have a value.");
			}

			if (this.questionItems.Count == 0 && this.questionType.Type != GlobalConstants.QuestionType.Free_text)
			{
				throw new InvalidSurveyException("Question must have question items.");
			}

			var question = new Question(
				this.questionDescription,
				this.questionIsRequired,
				this.questionType);

			this.questionItems.ForEach(x => question.AddQuestionItem(x));

			return question;
		}
	}
}
