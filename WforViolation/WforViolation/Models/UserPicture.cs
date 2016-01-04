using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class UserPicture:PictureInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallPath { get; set; }
        public string MediumPath { get; set; }
        public string LargePath { get; set; }
        public System.DateTime AddedDateTime { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public virtual Violation Violation { get; set; }

    }
}