namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class JobTitleData : IInitialData
    {
        public Type EntityType => typeof(JobTitle);

        public IEnumerable<object> GetData()
            => new List<JobTitle>
            {
                new JobTitle("Sales Specialist"),
                new JobTitle("Software Developer"),
                new JobTitle("Marketing Specialist"),
            };
    }
}
