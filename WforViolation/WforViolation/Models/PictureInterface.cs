using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
     public interface PictureInterface
    {
         string Name { get; set; }
         string SmallPath { get; set; }
         string MediumPath { get; set; }
         string LargePath { get; set; }
         System.DateTime AddedDateTime { get; set; }
    }
}