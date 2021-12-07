namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class QuestionTypeData : IInitialData
    {
        public Type EntityType => typeof(QuestionType);

        public IEnumerable<object> GetData()
            => new List<QuestionType>
            {
                new QuestionType("Checkbox"),
                new QuestionType("Free text"),
                new QuestionType("Radio"),
            };
    }
}
