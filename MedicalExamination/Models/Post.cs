using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Display(Name ="بوست")]
        public string PostContant { get; set; }
        public DateTime PostDate { get; set; }

        [Display(Name = "نوع البوست")]
        public int CategoryId { get; set; }

        public string PatientId { get; set; }

        public Category Category { get; set; }
        public virtual ApplicationUser Patient { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }  
    }
}