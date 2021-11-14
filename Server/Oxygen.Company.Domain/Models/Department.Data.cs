namespace Oxygen.Company.Domain.Models
{
    using Oxygen.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class DepartmentData : IInitialData
    {
        public Type EntityType => typeof(Department);

        public IEnumerable<object> GetData()
            => new List<Department>
            {
                new Department("Sales", true, "Markets the product."),
                new Department("Software Development", true, "Develops the product."),
                new Department("Marketing", true, "Sales the product."),
            };
    }
}
