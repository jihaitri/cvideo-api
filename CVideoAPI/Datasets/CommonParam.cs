using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Datasets
{
    public class CommonParam
    {
        public string OrderBy { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = int.MaxValue;
        public string Lang { get; set; } = "vi";
    }
}
