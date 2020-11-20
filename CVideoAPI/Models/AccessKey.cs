using CVideoAPI.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("AccessKey")]
    public class AccessKey : BaseEntity
    {
        public Guid DataKey { get; set; }
        [ForeignKey(nameof(Account))]
        public int UserId { get; set; }
        public Account Account { get; set; }
    }
}
