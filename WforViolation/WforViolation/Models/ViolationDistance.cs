using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class ViolationDistance
    {
        public int Id { get; set; }
        public string Distance { get; set; }

        public string SmallPath { get; set; }

        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public string Duration { get; set; }
    }
}