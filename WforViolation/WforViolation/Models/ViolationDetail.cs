using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class ViolationDetail
    {
        public ViolationDetail()
        {
            this.ViolationSubType = new HashSet<ViolationSubType>();
        }
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public PropertyType  PropertyType { get; set; }
        public double ? LowSeverityValue { get; set; }
        public double ? MediumSeverityValue { get; set; }
        public double ? HighSeverityValue { get; set; }
        public double ? CatastrophicSeverityValue { get; set; }

        public SeverityRelationType ? SeverityRelationType { get; set; }

        public virtual ICollection<ViolationSubType> ViolationSubType { get; set; }

    }
    public enum PropertyType
    {
        Integer,
        Double,
        Boolean
    }
    public enum SeverityRelationType
    {
        Ascending,
        Descending
    }
}