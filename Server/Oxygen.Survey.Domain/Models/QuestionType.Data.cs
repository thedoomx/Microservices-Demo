namespace Oxygen.Survey.Domain.Models
{
	using Oxygen.Common.Constants;
	using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class QuestionTypeData : IInitialData
    {
        public Type EntityType => typeof(QuestionType);

        public IEnumerable<object> GetData()
            => new List<QuestionType>
            {
                new QuestionType(GlobalConstants.QuestionType.Checkbox),
                new QuestionType(GlobalConstants.QuestionType.Free_text),
                new QuestionType(GlobalConstants.QuestionType.Radio),
            };
    }
}
