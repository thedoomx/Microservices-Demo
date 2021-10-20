namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using static Oxygen.Survey.Domain.Models.ModelConstants.Survey;

    public class Survey : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Question> questions;

        internal Survey(string name, string summary, SurveyType surveyType)
        {
            this.Validate(name, summary);

            this.Name = name;
            this.Summary = summary;
            this.SurveyType = surveyType;

            this.questions = new HashSet<Question>();
        }

        public string Name { get; private set; }

        public string Summary { get; private set; }

        public SurveyType SurveyType { get; private set; }

        public Survey ChangeName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        public Survey ChangeSummary(string summary)
        {
            this.ValidateSummary(summary);
            this.Summary = summary;

            return this;
        }

        public IReadOnlyCollection<Question> Questions => this.questions.ToList().AsReadOnly();

        public void AddQuestionItem(Question question)
        {
            this.questions.Add(question);

            //this.RaiseEvent(new QuestionAddedEvent());
        }

        private void Validate(string name, string summary)
        {
            this.ValidateName(name);
            this.ValidateSummary(summary);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidSurveyException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

        private void ValidateSummary(string summary)
            => Guard.ForStringLength<InvalidSurveyException>(
                summary,
                MinSummaryLength,
                MaxSummaryLength,
                nameof(this.Summary));
    }
}
