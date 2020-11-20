using System;

namespace CVideoAPI.Datasets.Employee
{
    public class EmployeeDataset
    {
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NumOfCVs { get; set; }
        public int NumOfVideos { get; set; }
    }
}
