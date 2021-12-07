namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class OfficeData : IInitialData
    {
        public Type EntityType => typeof(Office);

        public IEnumerable<object> GetData()
            => new List<Office>
            {
                new Office("Sofia", "Liylin, str. Tsaritsa Yoanna 333"),
                new Office("Burgas", "Meden Rudnik, str. Glarusite Katsat 13"),
                new Office("Plovdiv", "Karshiaka, str. Tam Ne Hodya 69"),
            };
    }
}
