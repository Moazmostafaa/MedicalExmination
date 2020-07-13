using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class City 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "المدينة")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "المحافظة")]
        public int GovernorateId { get; set; }

        [ForeignKey("GovernorateId")]
        public Governorate Governorate { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}