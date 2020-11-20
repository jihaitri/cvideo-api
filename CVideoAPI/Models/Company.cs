using CVideoAPI.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Company")]
    public class Company : BaseEntity, IProtectedAccess
    {
        [Key]
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [Required]
        [MaxLength(500)]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public Guid DataKey { get; private set; }
        public void SetDataKey(Guid key)
        {
            DataKey = key;
        }
    }
}
