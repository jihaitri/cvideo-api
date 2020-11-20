using CVideoAPI.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Section")]
    public class Section : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SectionId { get; set; }
        [Required]
        public string DisplayTitle { get; set; }
        [ForeignKey(nameof(SectionType))]
        public int SectionTypeId { get; set; }
        public SectionType SectionType { get; set; }
        [ForeignKey(nameof(CV))]
        public int CVId { get; set; }
        public CV CV { get; set; }
        public string Text { get; set; }
        public ICollection<SectionField> SectionFields { get; set; }
    }
}
