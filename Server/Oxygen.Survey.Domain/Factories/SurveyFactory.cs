namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;
    using System;
    using System.Collections.Generic;

    internal class SurveyFactory : ISurveyFactory
    {
        private string surveyName = default!;
        private string surveySummary = default!;

        private SurveyType surveyType = default!;
        private bool surveyTypeSet = false;

        private readonly List<Question> questions;

        internal SurveyFactory()
        {
            this.questions = new List<Question>();
        }

        public ISurveyFactory WithName(string name)
        {
            this.surveyName = name;
            return this;
        }

        public ISurveyFactory WithSummary(string summary)
        {
            this.surveySummary = summary;
            return this;
        }

        public ISurveyFactory WithSurveyType(string name)
            => this.WithSurveyType(new SurveyType(name));

        public ISurveyFactory WithSurveyType(SurveyType surveyType)
        {
            this.surveyType = surveyType;
            this.surveyTypeSet = true;
            return this;
        }

        public ISurveyFactory WithQuestion(Action<QuestionFactory> question)
        {
            var questionFactory = new QuestionFactory();

            question(questionFactory);

            this.questions.Add(questionFactory.Build());

            return this;
        }

        public Survey Build()
        {
            if (!this.surveyTypeSet)
            {
                throw new InvalidSurveyException("Survey type must have a value.");
            }

            var survey = new Survey(
                this.surveyName,
                this.surveySummary,
                this.surveyType);

            this.questions.ForEach(x => survey.AddQuestion(x));

            return survey;
        }
    }
}
