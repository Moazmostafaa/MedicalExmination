using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.TestAndDisease
{
    public class Test
    {
        public int Id { get; set; }
        [Required, Display(Name = "تاريخ الإنشاء")]
        public DateTime CreationDate { get; set; }
        public int DiseaseId { get; set; }
        [Required, Display(Name = "اسم المرض العربي")]
        public string DiseaseNameEn { get; set; }
        [Required, Display(Name = "اسم المرض الاجنبى")]
        public string DiseaseNameAr { get; set; }
        public string UserId { get; set; }
    }
}