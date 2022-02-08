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
	using Oxygen.Survey.Application.Queries.Common;
	using Oxygen.Application.Common.Exceptions;
	using Oxygen.Infrastructure.Common.Services;
	using Oxygen.Survey.Application.SurveyType.Queries.Common;
	using Oxygen.Survey.Application.QuestionType.Queries.Common;

	internal class SurveyRepository : DataRepository<ISurveyDbContext, Domain.Models.Survey>,
		ISurveyDomainRepository,
		ISurveyQueryRepository
	{
		private readonly IMapper mapper;

		public SurveyRepository(ISurveyDbContext db, IPublisher publisher, IMapper mapper)
			: base(db, publisher)
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

		public async Task<IEnumerable<SurveyOutputModel>> GetAll(CancellationToken cancellationToken = default)
			=> await this.mapper
			   .ProjectTo<SurveyOutputModel>(this
					.All())
			   .ToListAsync(cancellationToken);

		public async Task<IEnumerable<SurveyOutputModel>> Get(string userId, CancellationToken cancellationToken = default)
			=> await this.mapper
			   .ProjectTo<SurveyOutputModel>(this
					.Data
				.UserSurveys
				.Include(x => x.Survey)
				.Where(x => x.UserId == userId)
				.Select(x => x.Survey)
					.AsQueryable())
			   .ToListAsync(cancellationToken);

		public async Task<IEnumerable<SurveyTypeOutputModel>> SearchSurveyTypes(
		   CancellationToken cancellationToken = default)
		   => await this.mapper
			   .ProjectTo<SurveyTypeOutputModel>(this
					.Data
					.SurveyTypes
					.AsQueryable())
			   .ToListAsync(cancellationToken);

		public async Task<IEnumerable<QuestionTypeOutputModel>> SearchQuestionTypes(
		  CancellationToken cancellationToken = default)
		  => await this.mapper
			  .ProjectTo<QuestionTypeOutputModel>(this
				   .Data
				   .QuestionTypes
				   .AsQueryable())
			  .ToListAsync(cancellationToken);
	}
}
