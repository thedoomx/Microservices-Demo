namespace Oxygen.Company.Domain.Models
{
    public class ModelConstants
    {
        public class Common
        {
        }

        public class Office
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
            public const int MinAddressLength = 10;
            public const int MaxAddressLength = 1000;
        }

        public class Employee
        {
            public const int MinFirstNameLength = 2;
            public const int MaxFirstNameLength = 30;
            public const int MinSurNameLength = 2;
            public const int MaxSurNameLength = 30;
            public const int MinLastNameLength = 2;
            public const int MaxLastNameLength = 30;
        }

        public class Department
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 200;
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 1000;
        }

        public class JobTitle
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 200;
        }
    }
}
