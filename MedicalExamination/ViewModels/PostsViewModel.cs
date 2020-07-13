using MedicalExamination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalExamination.ViewModels
{
    public class PostsViewModel
    {
        public Patient Patient { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Post Post { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

    }
}