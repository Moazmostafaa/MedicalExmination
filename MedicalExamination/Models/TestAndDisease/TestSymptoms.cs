using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.TestAndDisease
{
    public class TestSymptoms
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int SymptomId { get; set; }

        public virtual ICollection<Symptoms> Symptoms { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}