using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("UserDevice")]
    public class UserDevice : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Account))]
        public int? AccountId { get; set; }
        public Account Account { get; set; }
        [Required]
        public string DeviceId { get; set; }
    }
}
