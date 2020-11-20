using CVideoAPI.Datasets.CV.Section.Field;
using System.Collections.Generic;

namespace CVideoAPI.Datasets.CV.Section
{
    public class NewSectionParam
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public int SectionTypeId { get; set; }
        public int CVId { get; set; }
        public string Text { get; set; }
        public List<CVFieldDataset> Fields { get; set; }
    }
}
