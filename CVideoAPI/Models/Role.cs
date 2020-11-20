using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
