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

        private string freeText = default!;

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

        public IEmployeeSurveyAnswerFactory WithFreeText(string freeText)
        {
            this.freeText = freeText;
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

            return new EmployeeSurveyAnswer(this.question, this.questionAnswer, this.freeText);
        }
    }
}
