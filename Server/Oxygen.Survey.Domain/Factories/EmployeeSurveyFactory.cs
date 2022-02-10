namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;
    using System;
    using System.Collections.Generic;

    internal class EmployeeSurveyFactory : IEmployeeSurveyFactory
    {
        private int employeeId = default!;
        private Survey survey = default!;
        private bool surveySet = false;
        private bool isSubmitted = default!;

        private List<EmployeeSurveyItem> employeeSurveyItems = new List<EmployeeSurveyItem>();

        public IEmployeeSurveyFactory WithEmployeeId(int employeeId)
        {
            this.employeeId = employeeId;
            return this;
        }

        public IEmployeeSurveyFactory WithSurvey(Survey survey)
        {
            this.survey = survey;
            this.surveySet = true;
            return this;
        }

        public IEmployeeSurveyFactory WithQuestionAnswer(Action<IEmployeeSurveyItemFactory> employeeSurveyItem)
        {
            var questionFactory = new EmployeeSurveyItemFactory();

            employeeSurveyItem(questionFactory);

            this.employeeSurveyItems.Add(questionFactory.Build());

            return this;
        }

        public EmployeeSurvey Build()
        {
            if (!this.surveySet)
            {
                throw new InvalidEmployeeSurveyException("Survey must have a value.");
            }

            var employeeSurvey = new EmployeeSurvey(
                this.employeeId,
                this.survey,
                this.isSubmitted);

            this.employeeSurveyItems.ForEach(x => employeeSurvey.AddEmployeeSurveyItem(x));

            return employeeSurvey;
        }
    }
}
