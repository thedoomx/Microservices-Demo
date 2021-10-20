namespace Oxygen.Survey.Domain.Models
{
    public class ModelConstants
    {
        public class Common
        {
        }

        public class Survey
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 1000;
            public const int MinSummaryLength = 5;
            public const int MaxSummaryLength = 1000;
        }

        public class SurveyType
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 30;
        }

        public class Question
        {
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 300;
        }

        public class QuestionType
        {
            public const int MinTypeLength = 10;
            public const int MaxTypeLength = 50;
        }

        public class QuestionItem
        {
            public const int MinDescriptionLength = 2;
            public const int MaxDescriptionLength = 300;
        }
    }
}
