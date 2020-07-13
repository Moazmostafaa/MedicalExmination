using MedicalExamination.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalExamination.ViewModels
{
    public class DoctorViewModel
    {
        public Doctor Doctor { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public DoctorHospital DoctorHospital { get; set; }
        public IEnumerable<DoctorHospital> DoctorHospitals { get; set; }
        public Hospital Hospital { get; set; }
        public IEnumerable<Hospital> Hospitals { get; set; }
        public Clinic Clinic { get; set; }
        public IEnumerable<Clinic> Clinics { get; set; }
        public string HospitalName { get; set; }
    }
}