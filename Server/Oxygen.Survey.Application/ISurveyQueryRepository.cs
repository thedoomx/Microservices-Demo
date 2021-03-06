namespace Oxygen.Survey.Application
{
    using Oxygen.Application.Common.Contracts;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Oxygen.Survey.Application.SurveyType.Queries.Common;
    using Oxygen.Survey.Application.QuestionType.Queries.Common;
	using Oxygen.Survey.Application.Survey.Queries.GetSubmit;
	using Oxygen.Survey.Application.Queries.Common;
	using Oxygen.Survey.Application.Survey.Queries.Mine;

	public interface ISurveyQueryRepository : IQueryRepository<Domain.Models.Survey>
    {
        Task<GetSubmitSurveyOutputModel> GetSubmitSurveyDetails(int id, CancellationToken cancellationToken = default);

        Task<SurveyOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<SurveyOutputModel>> GetAll(CancellationToken cancellationToken = default);

        Task<IEnumerable<MineSurveysOutputModel>> GetMine(int? employeeId, CancellationToken cancellationToken = default);

        Task<IEnumerable<SurveyTypeOutputModel>> SearchSurveyTypes(
           CancellationToken cancellationToken = default);

        Task<IEnumerable<QuestionTypeOutputModel>> SearchQuestionTypes(
          CancellationToken cancellationToken = default);
    }
}
