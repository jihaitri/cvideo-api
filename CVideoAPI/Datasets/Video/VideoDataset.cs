namespace CVideoAPI.Datasets.Video
{
    public class VideoDataset
    {
        public int VideoId { get; set; }
        public int CVId { get; set; }
        public int SectionId { get; set; }
        public string VideoUrl { get; set; }
        public string ThumbUrl { get; set; }
        public double AspectRatio { get; set; }
        public string CoverUrl { get; set; }
        public VideoStyleDataset VideoStyle { get; set; }
    }
}
