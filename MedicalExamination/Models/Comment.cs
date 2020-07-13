using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Display(Name = "تعليق")]
        public string CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; set; }
        public string DoctorId { get; set; }

        public virtual Post post { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
    }
}