namespace Oxygen.Survey.Application
{
    using Oxygen.Application.Common.Contracts;
    using Oxygen.Survey.Application.Queries.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Oxygen.Survey.Domain.Models;
    using Oxygen.Survey.Application.SurveyType.Queries.Common;
    using Oxygen.Survey.Application.QuestionType.Queries.Common;

    public interface ISurveyQueryRepository : IQueryRepository<Domain.Models.Survey>
    {
        Task<SurveyOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<SurveyOutputModel>> GetAll(CancellationToken cancellationToken = default);

        Task<IEnumerable<SurveyOutputModel>> GetMine(string? userId, CancellationToken cancellationToken = default);

        Task<IEnumerable<SurveyTypeOutputModel>> SearchSurveyTypes(
           CancellationToken cancellationToken = default);

        Task<IEnumerable<QuestionTypeOutputModel>> SearchQuestionTypes(
          CancellationToken cancellationToken = default);
    }
}
