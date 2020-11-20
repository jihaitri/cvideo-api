using CVideoAPI.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVideoAPI.Models
{
    [Table("RecruitmentPost")]
    public class RecruitmentPost : BaseEntity, IProtectedAccess
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PostId { get; set; }
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DueDate { get; set; }
        public int ExpectedNumber { get; set; }
        [Required]
        public string Title { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string JobBenefit { get; set; }
        [ForeignKey(nameof(Major))]
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public long MinSalary { get; set; }
        public long MaxSalary { get; set; }
        public ICollection<Application> Applications { get; set; }
        [Required]
        public Guid DataKey { get; private set; }
        public void SetDataKey(Guid key)
        {
            DataKey = key;
        }
    }
}
