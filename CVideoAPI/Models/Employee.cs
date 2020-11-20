using CVideoAPI.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Employee")]
    public class Employee : BaseEntity, IProtectedAccess
    {
        [Key]
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
        [Phone]
        public string Phone { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "yyyy/MM/dd")]
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public Guid DataKey { get; private set; }
        public void SetDataKey(Guid key)
        {
            DataKey = key;
        }
    }
}
