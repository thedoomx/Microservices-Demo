namespace Oxygen.Survey.Domain.Repositories
{
    using Oxygen.Domain.Common;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Oxygen.Survey.Domain.Models;
    using System.Threading;

    public interface ISurveyDomainRepository : IDomainRepository<Survey>
    {
        Task AddQuestion(Question question,
            CancellationToken cancellationToken = default);

        Task AddQuestionAnswer(QuestionAnswer questionAnswer,
            CancellationToken cancellationToken = default);

        Task<Survey> GetById(int id, CancellationToken cancellationToken = default);

        Task<SurveyType> GetSurveyTypeById(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<QuestionType>> GetQuestionTypes(CancellationToken cancellationToken = default);

        Task<Survey> GetSurveyWithQuestionsDataById(int id, CancellationToken cancellationToken = default);
    }
}
