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
	using Oxygen.Infrastructure.Common.Services;
	using Oxygen.Survey.Application.SurveyType.Queries.Common;
	using Oxygen.Survey.Application.QuestionType.Queries.Common;
	using Oxygen.Survey.Domain.Models;
	using Oxygen.Survey.Application.Survey.Queries.Submit;

	internal class SurveyRepository : DataRepository<ISurveyDbContext, Survey>,
		ISurveyDomainRepository,
		ISurveyQueryRepository
	{
		private readonly IMapper mapper;

		public SurveyRepository(ISurveyDbContext db, IPublisher publisher, IMapper mapper)
			: base(db, publisher)
			=> this.mapper = mapper;

		public async Task AddQuestion(Question question,
			CancellationToken cancellationToken = default)
		{
			await this.Data.Questions.AddAsync(question, cancellationToken);
		}

		public async Task AddQuestionItem(QuestionItem questionItem,
			CancellationToken cancellationToken = default)
		{
			await this.Data.QuestionItems.AddAsync(questionItem, cancellationToken);
		}

		public async Task<Survey> GetById(int id,
			CancellationToken cancellationToken = default)
		{
			return await this.All().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public async Task<SurveyType> GetSurveyTypeById(int id,
			CancellationToken cancellationToken = default)
			=> await this
				.Data
				.SurveyTypes
				.Where(x => x.Id == id)
				.FirstOrDefaultAsync(cancellationToken);

		public async Task<IEnumerable<QuestionType>> GetQuestionTypes(
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

		public async Task<Survey> GetSurveyWithQuestionsDataById(int id, CancellationToken cancellationToken = default)
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

		public async Task<IEnumerable<SurveyOutputModel>> GetMine(int? employeeId, CancellationToken cancellationToken = default)
			=> await this.mapper
			   .ProjectTo<SurveyOutputModel>(this
				.Data
				.EmployeeSurveys
				.Include(x => x.Survey)
				.Where(x => x.EmployeeId == employeeId)
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

		public async Task<SurveyOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
			 => await this.mapper
			   .ProjectTo<SurveyOutputModel>(this
					.All()
					.Where(x => x.Id == id)
					.Include(x => x.SurveyType))
			   .FirstOrDefaultAsync(cancellationToken);

		public async Task<SubmitSurveyOutputModel> GetSubmitSurveyDetails(int id, CancellationToken cancellationToken = default)
			 => await this.mapper
			   .ProjectTo<SubmitSurveyOutputModel>(this
					.All()
					.Where(x => x.Id == id)
					.Include(x => x.SurveyType)
					.Include(x => x.Questions)
					.ThenInclude(x => x.QuestionItems))
			   .FirstOrDefaultAsync(cancellationToken);
	}
}
