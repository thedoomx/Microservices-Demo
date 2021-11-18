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

    internal class SurveyRepository : DataRepository<ISurveyDbContext, Survey>,
        ISurveyDomainRepository,
        ISurveyQueryRepository
    {
        private readonly IMapper mapper;

        public SurveyRepository(ISurveyDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

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

        //public async Task<CarAd> Find(int id, CancellationToken cancellationToken = default)
        //    => await this
        //        .All()
        //        .Include(c => c.Manufacturer)
        //        .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        //public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        //{
        //    var carAd = await this.Data.CarAds.FindAsync(id);

        //    if (carAd == null)
        //    {
        //        return false;
        //    }

        //    this.Data.CarAds.Remove(carAd);

        //    await this.Data.SaveChangesAsync(cancellationToken);

        //    return true;
        //}

        //public async Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
        //    Specification<CarAd> carAdSpecification,
        //    Specification<Dealer> dealerSpecification,
        //    CarAdsSortOrder carAdsSortOrder,
        //    int skip = 0,
        //    int take = int.MaxValue,
        //    CancellationToken cancellationToken = default)
        //    => (await this.mapper
        //        .ProjectTo<TOutputModel>(this
        //            .GetCarAdsQuery(carAdSpecification, dealerSpecification)
        //            .Sort(carAdsSortOrder))
        //        .ToListAsync(cancellationToken))
        //        .Skip(skip)
        //        .Take(take); // EF Core bug forces me to execute paging on the client.

        //public async Task<CarAdDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
        //    => await this.mapper
        //        .ProjectTo<CarAdDetailsOutputModel>(this
        //            .AllAvailable()
        //            .Where(c => c.Id == id))
        //        .FirstOrDefaultAsync(cancellationToken);


        //public async Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCarAdCategories(
        //    CancellationToken cancellationToken = default)
        //{
        //    var categories = await this.mapper
        //        .ProjectTo<GetCarAdCategoryOutputModel>(this.Data.Categories)
        //        .ToDictionaryAsync(k => k.Id, cancellationToken);

        //    var carAdsPerCategory = await this.AllAvailable()
        //        .GroupBy(c => c.Category.Id)
        //        .Select(gr => new
        //        {
        //            CategoryId = gr.Key,
        //            TotalCarAds = gr.Count()
        //        })
        //        .ToListAsync(cancellationToken);

        //    carAdsPerCategory.ForEach(c => categories[c.CategoryId].TotalCarAds = c.TotalCarAds);

        //    return categories.Values;
        //}

        //public async Task<int> Total(
        //    Specification<CarAd> carAdSpecification,
        //    Specification<Dealer> dealerSpecification,
        //    CancellationToken cancellationToken = default)
        //    => await this
        //        .GetCarAdsQuery(carAdSpecification, dealerSpecification)
        //        .CountAsync(cancellationToken);

        //private IQueryable<CarAd> AllAvailable()
        //    => this
        //        .All()
        //        .Where(car => car.IsAvailable);

        //private IQueryable<CarAd> GetCarAdsQuery(
        //    Specification<CarAd> carAdSpecification,
        //    Specification<Dealer> dealerSpecification)
        //    => this
        //        .Data
        //        .Dealers
        //        .Where(dealerSpecification)
        //        .SelectMany(d => d.CarAds)
        //        .Where(carAdSpecification);

        public Task<IEnumerable<SurveyOutputModel>> Get(int? userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
