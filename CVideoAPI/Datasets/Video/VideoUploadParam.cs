namespace CVideoAPI.Datasets.Video
{
    public class VideoUploadParam
    {
        public int StyleId { get; set; }
        public int SectionId { get; set; }
        public string VideoUrl { get; set; }
        public string ThumbUrl { get; set; }
        public double AspectRatio { get; set; }
        public string CoverUrl { get; set; }
    }
}
