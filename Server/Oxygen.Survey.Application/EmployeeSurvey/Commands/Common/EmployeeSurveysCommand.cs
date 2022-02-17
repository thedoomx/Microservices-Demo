namespace Oxygen.Survey.Application.EmployeeSurvey.Commands.Common
{
    using Oxygen.Application.Common;
    using System.Collections.Generic;

    public abstract class EmployeeSurveysCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public EmployeeSurveysCommand()
        {
            this.EmployeeSurveys = new HashSet<EmployeeSurveyInputModel>();
        }

        public IEnumerable<EmployeeSurveyInputModel> EmployeeSurveys { get; set; }
    }
}
