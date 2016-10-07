using System;
using System.ComponentModel.DataAnnotations;

namespace ecrm.Domain.Model
{
    public class BaseDomainModel
    {
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
    }
}