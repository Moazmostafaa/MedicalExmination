using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.Doctor
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اسم العيادة")]
        public string ClinicName { get; set; }
        [Display(Name = "يوم")]
        public string DayName { get; set; }
        [Display(Name = "من")]
        public string From { get; set; }
        [Display(Name = "الي")]
        public string To { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        public string DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}