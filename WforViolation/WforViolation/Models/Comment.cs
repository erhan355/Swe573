using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class Comment
    {

        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PostedDateTime { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Violation Violation { get; set; }

    }
}