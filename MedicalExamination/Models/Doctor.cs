using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models.Doctor
{
    public class Doctor : ApplicationUser
    {
        public string CardId { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "التقييم")]
        public double? Total_Rate { get; set; }

        [Display(Name = "عدد التقييمات")]
        public long? UsersRated { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }

    public class DoctorEditProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر الحالية")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "الاسم")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "النوع")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "تاريخ الميلاد")]
        public String BirthDate { get; set; }

        [Required]
        [Display(Name = "المدينة")]
        public int CityId { get; set; }

    }
}