namespace Oxygen.Survey.Application.UserSurvey.Commands.Common
{
     using Oxygen.Application.Common;
    using System.Collections.Generic;

    public abstract class UserSurveyCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public UserSurveyCommand()
        {
            this.QuestionAnswers = new HashSet<UserSurveyItemInputModel>();
        }

        public string UserId { get; set; } = default!;

        public int SurveyId { get; set; }

        public IEnumerable<UserSurveyItemInputModel> QuestionAnswers { get; set; }
    }
}
