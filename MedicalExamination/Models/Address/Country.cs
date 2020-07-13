using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "البلد")]
        public string CountryName { get; set; }
    }
}