namespace Oxygen.Survey.Infrastructure.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Microsoft.EntityFrameworkCore;
	using Oxygen.Infrastructure.Common.Persistence;
	using Oxygen.Survey.Application;
	using Oxygen.Survey.Domain.Repositories;
	using Domain.Models;
	using Oxygen.Application.Common.Exceptions;
	using Oxygen.Infrastructure.Common.Services;
	using Oxygen.Survey.Application.EmployeeSurvey.Queries.Common;
	using Oxygen.Survey.Application.Queries.Common;

	internal class EmployeeSurveyRepository : DataRepository<ISurveyDbContext, EmployeeSurvey>,
		IEmployeeSurveyDomainRepository,
		IEmployeeSurveyQueryRepository
	{
		private readonly IMapper mapper;

		public EmployeeSurveyRepository(ISurveyDbContext db, IPublisher publisher, IMapper mapper)
			: base(db, publisher)
			=> this.mapper = mapper;

		public async Task AddEmployeeSurveyAnswer(EmployeeSurveyAnswer employeeSurveyAnswer,
			CancellationToken cancellationToken = default)
		{
			await this.Data.EmployeeSurveyAnswers.AddAsync(employeeSurveyAnswer, cancellationToken);
		}

		public async Task<EmployeeSurveyOutputModel> GetEmployeeSurveyDetails(int employeeSurveyId,
			CancellationToken cancellationToken = default)
			=> await this.mapper.ProjectTo<EmployeeSurveyOutputModel>(
			this.All()
			.Include(x => x.Survey)
			.Include(x => x.EmployeeSurveyAnswers).ThenInclude(x => x.Question).ThenInclude(x => x.QuestionType)
			.Include(x => x.EmployeeSurveyAnswers).ThenInclude(x => x.QuestionAnswer))
			.FirstOrDefaultAsync(x => x.Id == employeeSurveyId, cancellationToken);

		public async Task<EmployeeSurvey> GetById(int id,
			CancellationToken cancellationToken = default)
		=> await this.All()
			.Include(x => x.Survey)
			.Include(x => x.EmployeeSurveyAnswers).ThenInclude(x => x.Question)
			.Include(x => x.EmployeeSurveyAnswers).ThenInclude(x => x.QuestionAnswer)
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

		public async Task<EmployeeSurvey> GetByEmployeeIdAndSurveyId(int employeeId, int surveyId,
			CancellationToken cancellationToken = default)
			=> await this.All()
				.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Survey.Id == surveyId, cancellationToken);

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
