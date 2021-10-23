namespace Oxygen.Survey.Application
{
    using Oxygen.Application.Common.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISurveyQueryRepository : IQueryRepository<Oxygen.Survey.Domain.Models.Survey>
    {
        //Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
        //    Specification<CarAd> carAdSpecification,
        //    Specification<Dealer> dealerSpecification,
        //    CarAdsSortOrder carAdsSortOrder,
        //    int skip = 0,
        //    int take = int.MaxValue,
        //    CancellationToken cancellationToken = default);

        //Task<CarAdDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        //Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCarAdCategories(
        //    CancellationToken cancellationToken = default);

        //Task<int> Total(
        //    Specification<CarAd> carAdSpecification,
        //    Specification<Dealer> dealerSpecification,
        //    CancellationToken cancellationToken = default);
    }
}
