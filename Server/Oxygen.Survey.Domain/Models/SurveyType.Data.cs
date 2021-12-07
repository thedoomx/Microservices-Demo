namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class SurveyTypeData : IInitialData
    {
        public Type EntityType => typeof(SurveyType);

        public IEnumerable<object> GetData()
            => new List<SurveyType>
            {
                new SurveyType("Internal survey"),
                new SurveyType("Climate survey"),
                new SurveyType("Confirm order survey"),
            };
    }
}
