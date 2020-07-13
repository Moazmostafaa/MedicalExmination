using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class Governorate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "المحافظة")]
        public string GovernorateName { get; set; }

        [Required]
        [Display(Name = "الدولة")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}