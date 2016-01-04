using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class ViolationPicture:PictureInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallPath { get; set; }
        public string MediumPath { get; set; }
        public string LargePath { get; set; }
        public System.DateTime AddedDateTime { get; set; }
        public Violation Violation { get; set; }
    }
}