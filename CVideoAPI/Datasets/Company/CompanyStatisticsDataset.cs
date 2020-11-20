namespace CVideoAPI.Datasets.Company
{
    public class CompanyStatisticsDataset
    {
        public CompanyDataset Company { get; set; }
        public int TotalCandidates { get; set; }
        public int LastMonthCandidates { get; set; }
        public int TodayCandidates { get; set; }
        public int TotalRecruitmentPosts { get; set; }
    }
}
