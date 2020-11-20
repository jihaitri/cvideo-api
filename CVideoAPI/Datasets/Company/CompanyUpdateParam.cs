using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Datasets.Company
{
    public class CompanyUpdateParam
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
