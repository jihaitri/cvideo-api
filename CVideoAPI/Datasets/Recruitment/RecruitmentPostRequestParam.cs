namespace CVideoAPI.Datasets.Recruitment
{
    public class RecruitmentPostRequestParam
    {
        public string Location { get; set; } = string.Empty;
        public int MajorId { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public long MinSalary { get; set; } = 0;
        public long MaxSalary { get; set; } = long.MaxValue;
        public string Lang { get; set; } = "vi";
        public bool IsExpired { get; set; } = false;
        public string OrderBy { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = int.MaxValue;

        public bool ValidateSalary
        {
            get { return MinSalary <= MaxSalary; }
        }
    }
}
