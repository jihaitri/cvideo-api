using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string QuestionContent { get; set; }
        public int QuestionTime { get; set; }
        [ForeignKey(nameof(QuestionSet))]
        public int SetId { get; set; }
        public QuestionSet QuestionSet { get; set; }
        [Required]
        public int DispOrder { get; set; }
    }
}
