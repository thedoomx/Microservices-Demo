﻿namespace Oxygen.Survey.Infrastructure.Repositories
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

    internal class EmployeeSurveyRepository : DataRepository<ISurveyDbContext, EmployeeSurvey>,
        IEmployeeSurveyDomainRepository,
        IEmployeeSurveyQueryRepository
    {
        private readonly IMapper mapper;

        public EmployeeSurveyRepository(ISurveyDbContext db, IPublisher publisher, IMapper mapper)
            : base(db, publisher)
            => this.mapper = mapper;

        public async Task<EmployeeSurvey> GetById(int id,
            CancellationToken cancellationToken = default)
        {
            return await this.All().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AssignEmployeesToSurveyAsync(int surveyId, IEnumerable<string> employeeIds,
            CancellationToken cancellationToken = default)
        {
            var survey = await this.All().FirstOrDefaultAsync(x => x.Id == surveyId, cancellationToken);

            if (survey == null)
            {
                throw new NotFoundException("Survey", surveyId);
            }

            foreach (var employeeId in employeeIds)
            {

            }
        }
        
        public Task<IEnumerable<SurveyOutputModel>> Get(int? employeeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        
    }
}
