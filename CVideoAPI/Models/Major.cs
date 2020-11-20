using CVideoAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("Major")]
    public class Major : BaseEntity
    {
        [Key]
        public int MajorId { get; set; }
    }
}
