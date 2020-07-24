using MedicalExamination.Models.Doctor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.Doctor
{
    public class Rating
    {
        public int Id { get; set; }

        public virtual Doctor Doctor { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        public virtual Patient Patient { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        [Display(Name = "التقييم")]
        [Range(0.0, 5.0, ErrorMessage = "Please Enter Valid Rating Value Between 1 and 5")]
        [Required(ErrorMessage = "Please Enter Rating")]
        public int RatingValue { get; set; }

    }
}