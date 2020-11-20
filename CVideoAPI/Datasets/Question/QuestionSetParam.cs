namespace CVideoAPI.Datasets.Question
{
    public class QuestionSetParam
    {
        public int SetId { get; set; }
        public string SetName { get; set; }
        public int SectionTypeId { get; set; }
        public int Limit { get; set; } = int.MaxValue;
        public int Offset { get; set; } = 0;
    }
}
