using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Video")]
    public class Video : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoStyle))]
        public int StyleId { get; set; }
        public Style VideoStyle { get; set; }
        [ForeignKey(nameof(Section))]
        public int SectionId { get; set; }
        public Section Section { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        [Required]
        public string ThumbUrl { get; set; }
        [Required]
        public double AspectRatio { get; set; }
        [Required]
        public string CoverUrl { get; set; }
    }
}
