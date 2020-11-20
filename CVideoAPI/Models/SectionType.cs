using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("SectionType")]
    public class SectionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SectionTypeId { get; set; }
        [Required]
        public int DispOrder { get; set; }
        [Required]
        public string Image { get; set; }
        public ICollection<Translation> Translations { get; set; }
    }
}
