using System;

namespace CVideoAPI.Datasets.Question
{
    public class QuestionDataset
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public int QuestionTime { get; set; }
        public int DispOrder { get; set; }
        public QuestionSetDataset QuestionSet { get; set; }
        public DateTime Modified { get; set; }
    }
}
