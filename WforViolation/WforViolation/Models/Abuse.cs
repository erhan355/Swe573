using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WforViolation.Models
{
    public class Abuse
    {
        public int Id { get; set; }
        public int ViolationId { get; set; }

        public string NotifierId { get; set; }

        public DateTime NotificationDateTime { get; set; }
    }
}