namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserSurveyItem : Entity<int>
    {
        internal UserSurveyItem(int questionId, int questionItemId)
        {
            this.QuestionId = questionId;
            this.QuestionItemId = questionItemId;
        }

        public int QuestionId { get; private set; }

        public int QuestionItemId { get; private set; }
    }
}
