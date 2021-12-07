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

    public interface ISurveyQueryRepository : IQueryRepository<Domain.Models.Survey>
    {
        Task<IEnumerable<SurveyOutputModel>> Get(string? userId, CancellationToken cancellationToken = default);
    }
}
