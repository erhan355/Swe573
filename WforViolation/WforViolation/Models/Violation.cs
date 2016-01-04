using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class Violation
    {
        public Violation()
        {
            this.Comments = new HashSet<Comment>();
            this.ViolationPictures = new HashSet<ViolationPicture>();
        }
        public int Id { get; set; }
        [Required]
        public string MainType { get; set; }
        [Required]

        public string SubType { get; set; }

        public DateTime CreationDateTime { get; set; }
        [Required]
        public Severity Severity { get; set; }
        [Required]
        public string Longtitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }

        public int ViewCount { get; set; }

        public Status Status { get; set; }

        public int? LastEditorId { get; set; }

        public string MainViolationName { get; set; }

        public string SubViolationName { get; set; }

        public string ExternalMediaStorage { get; set; }

        public string DynamicValue { get; set; }

        public bool HasDynamic { get; set; }

        public string DynamicName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        //public virtual ViolationDetails ViolationDetails { get; set; }
        public virtual ICollection<ViolationPicture> ViolationPictures { get; set; }
        public virtual ApplicationUser CreatorUser { get; set; }

    }
    public enum Severity
    {
        NotAssessed,
        Low,
        Medium,
        High,
        Catastrophic

    }
    public enum Status
    {
        NotSolved,
        Solved

    }
    public enum DynamicType
    {
        Integer,
        Double

    }

}