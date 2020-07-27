using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.Doctor
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اسم المستشفي")]
        public string Name { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
    }
}