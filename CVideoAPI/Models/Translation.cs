using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Translation")]
    public class Translation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(NewsFeedSection))]
        public int? NewsFeedSectionId { get; set; }
        public NewsFeedSection NewsFeedSection { get; set; }
        [ForeignKey(nameof(SectionType))]
        public int? SectionTypeId { get; set; }
        public SectionType SectionType { get; set; }
        [ForeignKey(nameof(Major))]
        public int? MajorId { get; set; }
        public Major Major { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
