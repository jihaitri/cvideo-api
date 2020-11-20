using CVideoAPI.Datasets.CV.SectionType;
using System;
using System.Collections.Generic;

namespace CVideoAPI.Datasets.Question
{
    public class QuestionSetDataset
    {
        public int SetId { get; set; }
        public string SetName { get; set; }
        public DateTime Modified { get; set; }
        public CVSectionTypeDataset SectionType { get; set; }
        public List<QuestionDataset> Questions { get; set; }
    }
}
