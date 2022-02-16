namespace Oxygen.Survey.Domain.Factories
{
    using Oxygen.Survey.Domain.Exceptions;
    using Oxygen.Survey.Domain.Models;

    internal class EmployeeSurveyAnswerFactory : IEmployeeSurveyAnswerFactory
    {
        private Question question = default!;
        private bool questionSet = false;

        private QuestionAnswer questionAnswer = default!;
        private bool questionAnswerSet = default!;

        private string textValue = default!;
        private bool boolValue = default!;

        public IEmployeeSurveyAnswerFactory WithQuestion(Question question)
        {
            this.question = question;
            this.questionSet = true;

            return this;
        }

        public IEmployeeSurveyAnswerFactory WithQuestionAnswer(QuestionAnswer questionAnswer)
        {
            this.questionAnswer = questionAnswer;
            this.questionAnswerSet = true;

            return this;
        }

        public IEmployeeSurveyAnswerFactory WithTextValue(string textVal)
        {
            this.textValue = textVal;
            return this;
        }

        public IEmployeeSurveyAnswerFactory WithBoolValue(bool boolVal)
        {
            this.boolValue = boolVal;
            return this;
        }

        public EmployeeSurveyAnswer Build()
        {
            if (!this.questionSet)
            {
                throw new InvalidEmployeeSurveyAnswerException("Question must have a value.");
            }

            if (!this.questionAnswerSet)
            {
                throw new InvalidEmployeeSurveyAnswerException("Question answer must have a value.");
            }

            return new EmployeeSurveyAnswer(this.question, this.questionAnswer, this.textValue, this.boolValue);
        }
    }
}
