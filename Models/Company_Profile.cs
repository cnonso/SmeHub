using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SME_Hub.Models
{
    public class Company_Profile
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Business Name")]
        public string CompanyName { get; set; }


        [Required]
        public string Website { get; set; }


        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }


        [Required(ErrorMessage = "Business Classification is Required")]
        [Display(Name = "Business Classification")]
        public string Description { get; set; }


        [Required]
        public string Address { get; set; }

        [Required]
        public string Category { get; set; }

        public string LogoUrl { get; set; }



        [Display(Name = "Business Owner")]
        public string CompanyOwner { get; set; }


        [Display(Name = "Personal Contact Number")]
        public string PersonalPhoneNo { get; set; }


        [Display(Name = "Personal Email")]
        public string PersonalEmail { get; set; }


        [Display(Name = "Subscribe to Newsletter")]
        public bool SubscribeNewsletter { get; set; }

    }

    public partial class CompanyProfileViewModel
    {
        [Required]
        [Display(Name = "Business Name")]
        public string CompanyName { get; set; }


        [Required]
        public string Website { get; set; }


        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }


        [Required(ErrorMessage = "Business Classification is Required")]
        [Display(Name = "Business Classification")]
        public string Description { get; set; }


        [Required]
        public string Address { get; set; }

        [Required]
        public string Category { get; set; }
        public HttpPostedFileBase LogoImage { get; set; }




        [Display(Name = "Business Owner")]
        public string CompanyOwner { get; set; }


        [Display(Name = "Personal Contact Number")]
        public string PersonalPhoneNo { get; set; }


        [Display(Name = "Personal Email")]
        public string PersonalEmail { get; set; }


        [Display(Name = "Subscribe to Newsletter")]
        public bool SubscribeNewsletter { get; set; }
    }
}