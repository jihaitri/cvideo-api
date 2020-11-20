using CVideoAPI.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("CV")]
    public class CV : BaseEntity, IProtectedAccess
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CVId { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey(nameof(Major))]
        public int? MajorId { get; set; }
        public Major Major { get; set; }
        public ICollection<Application> Applications { get; set; }
        public Guid DataKey { get; private set; }
        public void SetDataKey(Guid key)
        {
            DataKey = key;
        }
    }
}
