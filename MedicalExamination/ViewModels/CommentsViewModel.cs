using MedicalExamination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalExamination.ViewModels
{
    public class CommentsViewModel
    {
        public Comment Comment { get; set; }
        public string OwnerName { get; set; }
    }
}