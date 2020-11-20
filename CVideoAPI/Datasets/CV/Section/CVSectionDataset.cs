using CVideoAPI.Datasets.CV.Section.Field;
using CVideoAPI.Datasets.Video;
using System.Collections.Generic;

namespace CVideoAPI.Datasets.CV.Section
{
    public class CVSectionDataset
    {
        public int SectionId { get; set; }
        public int SectionTypeId { get; set; }
        public int CVId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public List<CVFieldDataset> Fields { get; set; }
        public List<VideoDataset> Videos { get; set; }
    }
}
