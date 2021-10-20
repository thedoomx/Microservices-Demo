namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    internal class SurveyFactory : ISurveyFactory
    {
        private string surveyName = default!;
        private string surveySummary = default!;

        private SurveyType surveyType = default!;
        private bool surveyTypeSet = false;

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

        public Survey Build()
        {
            if (!this.surveyTypeSet)
            {
                throw new InvalidSurveyException("Survey type must have a value.");
            }

            return new Survey(
                this.surveyName,
                this.surveySummary,
                this.surveyType);
        }
    }
}
