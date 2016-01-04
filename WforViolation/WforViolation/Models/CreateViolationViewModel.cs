using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WforViolation.CustomValidations;

namespace WforViolation.Models
{
    public class CreateViolationViewModel
    {
        [Required(ErrorMessage = "You must choose Main Violation Type ")]
        [Display(Name = "Main Violation Type")]
        public string MainViolationType { get; set; }
        
        [Required(ErrorMessage = "You must choose Sub Violation Type ")]
        [Display(Name = "Sub Violation Type")]
        public string SubViolationType { get; set; }

        [Required(ErrorMessage = "Please Upload File")]
        [Display(Name = "Upload File")]
        [ValidateFile]
        public HttpPostedFileBase VImage { get; set; }

        [Required(ErrorMessage = "You must provide Latitude")]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "You must provide Longitude")]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "You must provide Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must write a title")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "Description")]
        [Required(ErrorMessage = "You must write a description")]
        public string Description { get; set; }

        public Severity Severity { get; set; }

        public string UploadedPicPath { get; set; }

        public string DynamicName { get; set; }

        public object DynamicValue { get; set; }
        [Display(Name = "Web Storage")]
        public string ExternalMediaStorage { get; set; }


    }

}