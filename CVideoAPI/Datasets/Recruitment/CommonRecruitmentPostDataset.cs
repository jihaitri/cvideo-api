using CVideoAPI.Datasets.Company;
using CVideoAPI.Datasets.Major;
using System;

namespace CVideoAPI.Datasets.Recruitment
{
    public class CommonRecruitmentPostDataset
    {
        public int PostId { get; set; }
        public CompanyDataset Company { get; set; }
        public string Location { get; set; }
        public int ExpectedNumber { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string JobBenefit { get; set; }
        public MajorDataset Major { get; set; }
        public long MinSalary { get; set; }
        public long MaxSalary { get; set; }
        public DateTime Created { get; set; }
        public bool IsApplied { get; set; } = false;
    }
}
