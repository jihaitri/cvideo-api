using CVideoAPI.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("NewsFeedSection")]
    public class NewsFeedSection : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int NewsFeedSectionId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Url { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int DispOrder { get; set; }
        public ICollection<Translation> Translations { get; set; }
    }
}
