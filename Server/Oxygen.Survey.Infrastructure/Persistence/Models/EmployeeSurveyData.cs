namespace Oxygen.Survey.Infrastructure.Persistence.Models
{
    using Survey.Domain.Models;
    using System.Collections.Generic;

    internal class EmployeeSurveyData
    {
        public EmployeeSurveyData()
        {
            this.EmployeeSurveyItems = new HashSet<EmployeeSurveyAnswer>();
        }

        public int Id { get; set; }

        public int EmployeeId { get; private set; } = default!;

        public int SurveyId { get; private set; }

        public bool IsSubmitted { get; private set; }

        public Survey Survey { get; set; } = default!;

        public IEnumerable<EmployeeSurveyAnswer> EmployeeSurveyItems { get; set; }
    }
}
