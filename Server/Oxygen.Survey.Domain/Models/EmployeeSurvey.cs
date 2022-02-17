namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeSurvey : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<EmployeeSurveyAnswer> employeeSurveyAnswers;

        internal EmployeeSurvey(int employeeId, Survey survey, bool isSubmitted)
        {
            this.EmployeeId = employeeId;
            this.Survey = survey;
            this.IsSubmitted = isSubmitted;

            this.employeeSurveyAnswers = new HashSet<EmployeeSurveyAnswer>();
        }

        private EmployeeSurvey(int employeeId, bool isSubmitted)
        {
            this.EmployeeId = employeeId;
            this.IsSubmitted = isSubmitted;

            this.employeeSurveyAnswers = new HashSet<EmployeeSurveyAnswer>();
        }

        public int EmployeeId { get; private set; }

        public Survey Survey { get; private set; }

        public bool IsSubmitted { get; private set; }

        public EmployeeSurvey Submit()
        {
            this.IsSubmitted = true;

            return this;
        }

        public IReadOnlyCollection<EmployeeSurveyAnswer> EmployeeSurveyAnswers => 
            this.employeeSurveyAnswers.ToList().AsReadOnly();

        public void AddEmployeeSurveyAnswer(EmployeeSurveyAnswer employeeSurveyAnswer)
        {
            this.employeeSurveyAnswers.Add(employeeSurveyAnswer);
        }
    }
}
