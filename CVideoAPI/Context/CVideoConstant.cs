namespace CVideoAPI.Context
{
    public class CVideoConstant
    {
        public struct Roles
        {
            public struct Admin
            {
                public const string Name = "admin";
                public const int Id = 1;
            }
            public struct Employee
            {
                public const string Name = "employee";
                public const int Id = 2;
            }
            public struct Employer
            {
                public const string Name = "employer";
                public const int Id = 3;
            }
        }

        public struct Client
        {
            public const string Admin = "0";
            public const string Employee = "1";
            public const string Employer = "2";
        }
    }
}
