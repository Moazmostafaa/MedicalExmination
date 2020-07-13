using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalExamination.Models
{
    public class Patient : ApplicationUser
    {
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر الحالية")]
        public string  CurrentPassword { get; set; }

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