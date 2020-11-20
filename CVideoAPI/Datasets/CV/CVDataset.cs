using CVideoAPI.Datasets.CV.Section;
using CVideoAPI.Datasets.Employee;
using CVideoAPI.Datasets.Major;
using System;
using System.Collections.Generic;

namespace CVideoAPI.Datasets.CV
{
    public class CVDataset
    {
        public int CVId { get; set; }
        public string Title { get; set; }
        public EmployeeDataset Employee { get; set; }
        public List<CVSectionDataset> Sections { get; set; }
        public MajorDataset Major { get; set; }
        public DateTime Created { get; set; }
    }
}
