namespace Oxygen.Survey.Application.Commands.Common
{
    using Oxygen.Application.Common;
    using System.Collections.Generic;

    public abstract class SurveyCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public SurveyCommand()
        {
            this.Questions = new HashSet<QuestionInputModel>();
        }

        public string Name { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public int SurveyType { get; set; }

        public IEnumerable<QuestionInputModel> Questions { get; set; }
    }
}