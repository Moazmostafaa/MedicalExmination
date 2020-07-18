using MedicalExamination.Models.TestAndDisease;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.ViewModels.DiseasesAndSymptoms
{
    public class SymptomsViewModel
    {
        public Symptoms Symptom { get; set; }
        [Display(Name = ("اسم الفئة العربي"))]
        public string CategoryNameAr { get; set; }
    }
}