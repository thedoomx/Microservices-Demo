﻿namespace Oxygen.Company.Domain.Repositories
{
     using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Oxygen.Company.Domain.Models;
    using System.Threading;

    public interface IEmployeeDomainRepository : IDomainRepository<Employee>
    {
        //Task<Survey> FindByUser(string userId, CancellationToken cancellationToken = default);

        //Task<int> GetDealerId(string userId, CancellationToken cancellationToken = default);

        //Task<bool> HasCarAd(int dealerId, int carAdId, CancellationToken cancellationToken = default);
    }
}