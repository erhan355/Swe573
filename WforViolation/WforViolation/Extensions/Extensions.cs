using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WforViolation.Models;

namespace WforViolation.Extensions
{
    public class Extensions
    {
        public static IEnumerable<SelectListItem> GetMainViolationList()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            IList<SelectListItem> mV = new List<SelectListItem>();
            List<ViolationMainType> mainViolations = context.ViolationMainTypes.ToList();
            foreach (var item in mainViolations)
            {
                mV.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return mV;

        }
        public static IEnumerable<SelectListItem> GetSubOfMainViolations(string mainViolationName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ViolationMainType mainViolationType = context.ViolationMainTypes.Where(x => x.Name == mainViolationName).FirstOrDefault();
            List<ViolationSubType> subViolations = context.ViolationSubTypes.Where(x => x.ViolationMainType.Id == mainViolationType.Id).ToList();
            IList<SelectListItem> mV = new List<SelectListItem>();
            foreach (var item in subViolations)
            {
                mV.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return mV;

        }
        public static IEnumerable<SelectListItem> GetSeverityList()
        {
            IList<SelectListItem> mV = new List<SelectListItem>();
            
            mV.Add(new SelectListItem() { Text = "Low", Value = "1"});
            mV.Add(new SelectListItem() { Text = "Medium", Value = "2" });
            mV.Add(new SelectListItem() { Text = "High", Value = "3" });
            mV.Add(new SelectListItem() { Text = "Catastrophic", Value = "4" });
            mV.Add(new SelectListItem() { Text = "NotAssessed", Value = "5" });
            return mV;

        }
    }
}