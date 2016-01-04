using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class ViolationSubType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ViolationDetail ViolationDetail { get; set; }
        public virtual ViolationMainType ViolationMainType { get; set; }

    }
}