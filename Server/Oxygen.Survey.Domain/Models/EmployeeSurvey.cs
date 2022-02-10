namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeSurvey : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<EmployeeSurveyItem> employeeSurveyItems;

        internal EmployeeSurvey(int employeeId, Survey survey, bool isSubmitted)
        {
            this.EmployeeId = employeeId;
            this.Survey = survey;
            this.IsSubmitted = isSubmitted;

            this.employeeSurveyItems = new HashSet<EmployeeSurveyItem>();
        }

        private EmployeeSurvey(int employeeId, bool isSubmitted)
        {
            this.EmployeeId = employeeId;
            this.IsSubmitted = isSubmitted;

            this.employeeSurveyItems = new HashSet<EmployeeSurveyItem>();
        }

        public int EmployeeId { get; private set; }

        public Survey Survey { get; private set; }

        public bool IsSubmitted { get; private set; }

        public IReadOnlyCollection<EmployeeSurveyItem> EmployeeSurveyItems => 
            this.employeeSurveyItems.ToList().AsReadOnly();

        public void AddEmployeeSurveyItem(EmployeeSurveyItem employeeSurveyItem)
        {
            this.employeeSurveyItems.Add(employeeSurveyItem);
        }
    }
}
