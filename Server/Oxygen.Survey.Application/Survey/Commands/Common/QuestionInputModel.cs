namespace Oxygen.Survey.Application.Survey.Commands.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class QuestionInputModel
    {
        public QuestionInputModel()
        {
            this.QuestionItems = new HashSet<QuestionItemInputModel>();
        }

        public string Description { get; set; } = default!;

        public bool IsRequired { get; set; }

        public int QuestionType { get; set; }

        public IEnumerable<QuestionItemInputModel> QuestionItems { get; set; }
    }
}
