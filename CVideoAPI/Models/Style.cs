using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Style")]
    public class Style
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StyleId { get; set; }
        [Required]
        [MaxLength(100)]
        public string StyleName { get; set; }
    }
}
