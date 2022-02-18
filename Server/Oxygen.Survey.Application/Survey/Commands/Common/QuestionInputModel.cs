namespace Oxygen.Survey.Application.Survey.Commands.Common
{
    using System.Collections.Generic;

    public class QuestionInputModel
    {
        public QuestionInputModel()
        {
            this.QuestionAnswers = new HashSet<QuestionAnswerInputModel>();
        }

        public string Description { get; set; } = default!;

        public bool IsRequired { get; set; }

        public int QuestionType { get; set; }

        public IEnumerable<QuestionAnswerInputModel> QuestionAnswers { get; set; }
    }
}
