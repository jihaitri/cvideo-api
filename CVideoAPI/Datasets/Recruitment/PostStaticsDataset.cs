using CVideoAPI.Datasets.Company;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Major;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Datasets.Recruitment
{
    public class PostStaticsDataset
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public int ExpectedNumber { get; set; }
        public DateTime DueDate { get; set; }
        public int MajorId { get; set; }
        public MajorDataset Major { get; set; }
        public long MinSalary { get; set; }
        public long MaxSalary { get; set; }
        public DateTime Created { get; set; }
        public int TotalCVs { get; set; }
        public int NewCVs { get; set; }
    }
}
