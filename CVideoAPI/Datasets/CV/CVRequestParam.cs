namespace CVideoAPI.Datasets.CV
{
    public class CVRequestParam
    {
        public string Title { get; set; } = string.Empty;
        public int MajorId { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public string OrderBy { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = int.MaxValue;
    }
}
