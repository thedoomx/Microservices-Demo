namespace Oxygen.Survey.Infrastructure.Persistence.Models
{
    using Survey.Domain.Models;

    internal class UserSurveyData
    {
        public int Id { get; set; }

        public string UserId { get; private set; } = default!;

        public int SurveyId { get; private set; }

        public bool IsSubmitted { get; private set; }

        public Survey Survey { get; set; } = default!;
    }
}
