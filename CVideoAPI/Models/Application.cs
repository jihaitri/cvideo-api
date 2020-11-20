using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Application")]
    public class Application : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ApplicationId { get; set; }
        public int PostId { get; set; }
        public RecruitmentPost RecruitmentPost { get; set; }
        public int CVId { get; set; }
        public CV CV { get; set; }
        public bool Viewed { get; set; }
    }
}
