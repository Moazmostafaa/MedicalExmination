using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.TestAndDisease
{
    public class DiseaseSymptoms
    {
        [Key]
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int SymptomId { get; set; }

        public virtual ICollection<Symptoms> Symptoms { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
    }
}