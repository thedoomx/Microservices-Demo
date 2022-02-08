namespace Oxygen.Survey.Application.UserSurvey.Commands.Common
{
    using Oxygen.Application.Common;
    using System.Collections.Generic;

    public abstract class UserSurveysCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public UserSurveysCommand()
        {
            this.UserSurveys = new HashSet<UserSurveyInputModel>();
        }

        public IEnumerable<UserSurveyInputModel> UserSurveys { get; set; }
    }
}
