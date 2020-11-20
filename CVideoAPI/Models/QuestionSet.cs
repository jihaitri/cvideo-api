using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("QuestionSet")]
    public class QuestionSet : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SetId { get; set; }
        [Required]
        public string SetName { get; set; }
        [ForeignKey(nameof(SectionType))]
        public int SectionTypeId { get; set; }
        public SectionType SectionType { get; set; }
    }
}
