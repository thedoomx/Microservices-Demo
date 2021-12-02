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
    using Oxygen.Survey.Application.Queries.Common;
    using Oxygen.Application.Common.Exceptions;

    internal class SurveyRepository : DataRepository<ISurveyDbContext, Domain.Models.Survey>,
        ISurveyDomainRepository,
        ISurveyQueryRepository
    {
        private readonly IMapper mapper;

        public SurveyRepository(ISurveyDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<Domain.Models.Survey> GetById(int id,
            CancellationToken cancellationToken = default)
        {
            return await this.All().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Domain.Models.SurveyType> GetSurveyTypeById(int id,
            CancellationToken cancellationToken = default)
            => await this
                .Data
                .SurveyTypes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<Domain.Models.QuestionType>> GetQuestionTypes(
            CancellationToken cancellationToken = default)
            => await this
                .Data
                .QuestionTypes
                .ToListAsync(cancellationToken);

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

        public async Task<Domain.Models.Survey> GetSurveyWithQuestionsDataById(int id, CancellationToken cancellationToken = default)
        => await this
                .All()
                .Where(x => x.Id == id)
                .Include(x => x.Questions)
                .ThenInclude(x => x.QuestionItems)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<SurveyOutputModel>> Get(string userId, CancellationToken cancellationToken = default)
            => await this.mapper
               .ProjectTo<SurveyOutputModel>(this
                    .Data
                .UserSurveys
                .Where(x => x.UserId == userId)
                .Select(x => x.Survey)
                    .AsQueryable())
               .ToListAsync(cancellationToken);
    }
}
