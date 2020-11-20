using System;
using System.ComponentModel.DataAnnotations;

namespace CVideoAPI.Models.BaseModels
{
    public class BaseEntity
    {
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
    }
}
