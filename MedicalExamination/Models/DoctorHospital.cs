using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.Doctor
{
    public class DoctorHospital
    {
        [Key]
        public int Id { get; set; }
        public string DayName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DoctorId { get; set; }
        public int HospitalId { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
    }
}