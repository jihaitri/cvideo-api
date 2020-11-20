using CVideoAPI.Datasets.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Datasets.Application
{
    public class ApplicationDataset
    {
        public int ApplicationId { get; set; }
        public CVDataset CV { get; set; }
        public int PostId { get; set; }
        public bool Viewed { get; set; }
        public DateTime Created { get; set; }
    }
}
