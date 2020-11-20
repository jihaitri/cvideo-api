using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Datasets.Application
{
    public class ApplicationParam
    {
        public bool? Viewed { get; set; }
        public string OrderBy { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = int.MaxValue;
    }
}
