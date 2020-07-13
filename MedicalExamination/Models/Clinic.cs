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
        public string ClinicName { get; set; }
        public string DayName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Address { get; set; }
        public string DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}