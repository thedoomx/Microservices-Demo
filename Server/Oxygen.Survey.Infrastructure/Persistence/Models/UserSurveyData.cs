namespace Oxygen.Survey.Infrastructure.Persistence.Models
{
    using Survey.Domain.Models;
    using System.Collections.Generic;

    internal class UserSurveyData
    {
        public UserSurveyData()
        {
            this.UserSurveyItems = new HashSet<UserSurveyItem>();
        }

        public int Id { get; set; }

        public string UserId { get; private set; } = default!;

        public int SurveyId { get; private set; }

        public bool IsSubmitted { get; private set; }

        public Survey Survey { get; set; } = default!;

        public IEnumerable<UserSurveyItem> UserSurveyItems { get; set; }
    }
}
