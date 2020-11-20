using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Datasets.Recruitment
{
    public class RecruitmentPostToUpdateParam
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime DueDate { get; set; }
        public int ExpectedNumber { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string JobBenefit { get; set; }
        public int MajorId { get; set; }
        public long MaxSalary { get; set; }
        public long MinSalary { get; set; }
    }
}
