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

        Task AddQuestionItem(QuestionItem questionItem,
            CancellationToken cancellationToken = default);

        Task<Survey> GetById(int id, CancellationToken cancellationToken = default);

        Task<SurveyType> GetSurveyTypeById(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<QuestionType>> GetQuestionTypes(CancellationToken cancellationToken = default);

        Task<Survey> GetSurveyWithQuestionsDataById(int id, CancellationToken cancellationToken = default);

        //Task<Survey> FindByUser(string userId, CancellationToken cancellationToken = default);

        //Task<int> GetDealerId(string userId, CancellationToken cancellationToken = default);

        //Task<bool> HasCarAd(int dealerId, int carAdId, CancellationToken cancellationToken = default);
    }
}
