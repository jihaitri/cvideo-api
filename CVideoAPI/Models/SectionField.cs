using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("SectionField")]
    public class SectionField : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FieldId { get; set; }
        [Required]
        public string FieldTitle { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public string Text { get; set; }
    }
}
