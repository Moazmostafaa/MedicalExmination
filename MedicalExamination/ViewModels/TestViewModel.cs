using MedicalExamination.Filters;
using MedicalExamination.Models.Doctor;
using MedicalExamination.Models.TestAndDisease;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalExamination.ViewModels
{
    public class TestViewModel
    {
        public Test Test { get; set; }
        public List<Symptoms> Symptoms { get; set; }
        public List<Doctor> Doctors { get; set; }
        public TestFilter TestFilter { get; set; }
    }
}