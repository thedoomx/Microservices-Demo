namespace Oxygen.Survey.Domain.Factories
{
	using Oxygen.Common.Constants;
	using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;
	using System;
	using System.Collections.Generic;

	public class QuestionFactory
    {
        private QuestionType questionType = default!;
        private bool questionTypeSet = false;

        private string questionDescription = default!;
        private bool questionIsRequired = default!;

        private List<QuestionItem> questionItems = new List<QuestionItem>();

        public QuestionFactory WithDescription(string description)
        {
            this.questionDescription = description;
            return this;
        }

        public QuestionFactory WithRequired(bool isRequired)
        {
            this.questionIsRequired = isRequired;
            return this;
        }

        public QuestionFactory WithQuestionType(string type)
            => this.WithQuestionType(new QuestionType(type));

        public QuestionFactory WithQuestionType(QuestionType questionType)
        {
            this.questionType = questionType;
            this.questionTypeSet = true;
            return this;
        }

        public QuestionFactory WithQuestionItem(Action<QuestionItemFactory> questionItem)
        {
            var questionItemFactory = new QuestionItemFactory();

            questionItem(questionItemFactory);

            this.questionItems.Add(questionItemFactory.Build());

            return this;
        }

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
