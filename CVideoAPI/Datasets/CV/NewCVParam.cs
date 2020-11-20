using System.ComponentModel.DataAnnotations;

namespace CVideoAPI.Datasets.CV
{
    public class NewCVParam
    {
        public int CVId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int MajorId { get; set; }
    }
}
