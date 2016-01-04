using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class ViolationMainType
    {
        public ViolationMainType()
        {
            this.ViolationSubTypes = new HashSet<ViolationSubType>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ViolationSubType> ViolationSubTypes { get; set; }
    }
}