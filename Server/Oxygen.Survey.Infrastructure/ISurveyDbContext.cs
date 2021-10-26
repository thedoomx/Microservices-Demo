using Oxygen.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oxygen.Survey.Infrastructure
{
    internal interface ISurveyDbContext : IDbContext
    {
        //DbSet<CarAd> CarAds { get; }

        //DbSet<Category> Categories { get; }

        //DbSet<Manufacturer> Manufacturers { get; }

        //DbSet<Dealer> Dealers { get; }

        //DbSet<User> Users { get; } // TODO: Temporary workaround
    }
}
