namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common.Models;
    using Oxygen.Survey.Domain.Exceptions;
    using static Oxygen.Survey.Domain.Models.ModelConstants.SurveyType;


    public class SurveyType : Entity<int>
    {
        internal SurveyType(string name)
        {
            this.Validate(name);
            this.Name = name;
        }

        public string Name { get; private set; }

        public SurveyType ChangeName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }


        private void Validate(string name)
        {
            this.ValidateName(name);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidSurveyTypeException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

    }
}
