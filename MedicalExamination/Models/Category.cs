using MedicalExamination.Models.TestAndDisease;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="التخصص")]
        public string CategoryName { get; set; }
        [Required, Display(Name = "تاريخ الإنشاء")]
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Symptoms> Symptoms { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<ApplicationUser> Doctor { get; set; }
    }
}