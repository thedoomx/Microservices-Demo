namespace Oxygen.Survey.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Survey.Application;
    using Oxygen.Survey.Domain.Repositories;
    using Domain.Models;
    using Oxygen.Survey.Application.Queries.Common;
    using Oxygen.Application.Common.Exceptions;
    using Oxygen.Infrastructure.Common.Services;

    internal class UserSurveyRepository : DataRepository<ISurveyDbContext, UserSurvey>,
        IUserSurveyDomainRepository,
        IUserSurveyQueryRepository
    {
        private readonly IMapper mapper;

        public UserSurveyRepository(ISurveyDbContext db, IPublisher publisher, IMapper mapper)
            : base(db, publisher)
            => this.mapper = mapper;

        public async Task<UserSurvey> GetById(int id,
            CancellationToken cancellationToken = default)
        {
            return await this.All().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AssignUsersToSurveyAsync(int surveyId, IEnumerable<string> userIds,
            CancellationToken cancellationToken = default)
        {
            var survey = await this.All().FirstOrDefaultAsync(x => x.Id == surveyId, cancellationToken);

            if (survey == null)
            {
                throw new NotFoundException("Survey", surveyId);
            }

            foreach (var userId in userIds)
            {

            }
        }
        
        public Task<IEnumerable<SurveyOutputModel>> Get(int? userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        
    }
}
