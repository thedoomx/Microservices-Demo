namespace Oxygen.Survey.Domain.Repositories
{
    using Oxygen.Domain.Common;
    using Oxygen.Survey.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserSurveyDomainRepository : IDomainRepository<UserSurvey>
    {
        Task<UserSurvey> GetById(int id, CancellationToken cancellationToken = default);
    }
}
