using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.TestAndDisease
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }
        [Required,Display(Name ="الاسم العربي")]
        public string NameAr { get; set; }
        [Required, Display(Name = "الاسم الاجنبى")]
        public string NameEn { get; set; }
        [Required, Display(Name = "تاريخ الإنشاء")]
        public DateTime CreationDate { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}