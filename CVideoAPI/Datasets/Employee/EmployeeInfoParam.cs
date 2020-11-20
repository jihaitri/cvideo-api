using System;

namespace CVideoAPI.Datasets.Employee
{
    public class EmployeeInfoParam
    {
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
