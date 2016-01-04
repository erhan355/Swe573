using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WforViolation.Helpers;
using WforViolation.Models;
using PagedList;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
namespace WforViolation.Controllers
{
    public class ViolationController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index(int id, int pagination = 1, string nearestSelected = "AllDistances", string selectedSubCategory = null, string selectedSeverity = null, string currentLatitude = "NoneLat", string currentLongitutude = "NoneLon")
        {
            ViewBag.ViolationCategoryName = context.ViolationMainTypes.Where(x => x.Id == id).FirstOrDefault().Name;
            ViewBag.ViolationCategoryId = context.ViolationMainTypes.Where(x => x.Id == id).FirstOrDefault().Id;
            ViewBag.nearestSelected = nearestSelected;
            ViewBag.selectedSubCategory = selectedSubCategory;
            ViewBag.selectedSeverity = selectedSeverity;
            string idString = id.ToString();

            List<Violation> violationsList = new List<Violation>();
            violationsList = context.Violations.Where(x => x.MainType == idString).ToList();

            if (violationsList.Count() > 0 && selectedSubCategory != null && selectedSubCategory != "undefined" && selectedSubCategory != "AllSubCategories" && selectedSubCategory != "")
            {
                violationsList = violationsList.Where(x => x.SubType == selectedSubCategory).ToList();
            }
            if (violationsList.Count() > 0 && selectedSeverity != null && selectedSeverity != "undefined" && selectedSeverity != "AllSeverity" && selectedSeverity != "")
            {
                int severityId = Convert.ToInt32(selectedSeverity);
                if (severityId == 1)//Low
                {
                    violationsList = violationsList.Where(x => x.Severity == Severity.Low).ToList();
                }
                else if (severityId == 2)//Medium
                {
                    violationsList = violationsList.Where(x => x.Severity == Severity.Medium).ToList();
                }
                else if (severityId == 3)//High
                {
                    violationsList = violationsList.Where(x => x.Severity == Severity.High).ToList();
                }
                else if (severityId == 4)//Catastrophic
                {
                    violationsList = violationsList.Where(x => x.Severity == Severity.Catastrophic).ToList();
                }
                else if (severityId == 5)//NotAssessed
                {
                    violationsList = violationsList.Where(x => x.Severity == Severity.NotAssessed).ToList();
                }
            }
            if (violationsList.Count() > 0 && nearestSelected != null && nearestSelected != "undefined" && nearestSelected != "AllDistances" && nearestSelected != "" && currentLatitude != "NoneLat" && currentLongitutude != "NoneLon")
            {
                currentLatitude = currentLatitude.Replace(",", ".");
                currentLongitutude = currentLongitutude.Replace(",", ".");
                string origin = currentLatitude + "," + currentLongitutude;
                List<int> distances = new List<int>();
                List<Violation> violationsListDistanceBased = new List<Violation>();
                foreach (var item in violationsList)
                {

                    string destination = item.Latitude + "," + item.Longtitude;
                    // Build your request to the API
                    var request = WebRequest.Create("https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origin + "&destinations=" + destination + "&mode=walking");
                    // Indicate you are looking for a JSON response
                    request.ContentType = "application/json; charset=utf-8";
                    var response = (HttpWebResponse)request.GetResponse();

                    // Read through the response
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        // Define a serializer to read your response
                        JavaScriptSerializer serializer = new JavaScriptSerializer();

                        // Get your results
                        dynamic result = serializer.DeserializeObject(sr.ReadToEnd());

                        // Read the distance property from the JSON request
                        int? distance = result["rows"][0]["elements"][0]["distance"]["value"]; // yields "1,300 KM" 
                        var duration = result["rows"][0]["elements"][0]["duration"]["text"];
                        var destinationAddress = result["destination_addresses"];
                        if (distance != null && distance < Convert.ToInt32(nearestSelected))
                        {
                            violationsListDistanceBased.Add(item);
                        }
                    }

                }
                return View(violationsListDistanceBased.OrderByDescending(x => x.CreationDateTime).ToPagedList(pagination, 1));


            }


            return View(violationsList.OrderByDescending(x => x.CreationDateTime).ToPagedList(pagination, 1));

        }
        public ActionResult RetrieveMainViolationTypesWidget()
        {

            return View(context.ViolationMainTypes.ToList());
        }
        public ActionResult RetrieveMainViolationTypesHome()
        {

            return View(context.ViolationMainTypes.ToList());
        }
        public ActionResult Create()
        {
            //context.
            return View();

        }
        [HttpPost]
        public ActionResult Create(CreateViolationViewModel createViolationViewModel)
        {
            int mainViolationId = Convert.ToInt32(createViolationViewModel.MainViolationType);
            int subViolationId = Convert.ToInt32(createViolationViewModel.SubViolationType);
            Violation violation = new Violation();
            violation.MainType = createViolationViewModel.MainViolationType;
            violation.SubType = createViolationViewModel.SubViolationType;
            violation.CreationDateTime = DateTime.Now;
            ViolationPicture violationPicture = new ViolationPicture();
            violationPicture = ImageHelper.SavePic(createViolationViewModel.VImage, HttpContext, violationPicture) as ViolationPicture;
            violation.ViolationPictures.Add(violationPicture);
            violation.Severity = createViolationViewModel.Severity;
            violation.Latitude = createViolationViewModel.Latitude;
            violation.Longtitude = createViolationViewModel.Longitude;
            violation.Title = createViolationViewModel.Title;
            violation.Description = createViolationViewModel.Description;
            violation.Address = createViolationViewModel.Address;
            violation.ViewCount = 0;
            violation.Status = Status.NotSolved;
            violation.ExternalMediaStorage = createViolationViewModel.ExternalMediaStorage;
            violation.MainViolationName = context.ViolationMainTypes.Where(x => x.Id == mainViolationId).FirstOrDefault().Name;
            violation.SubViolationName = context.ViolationSubTypes.Where(x => x.Id == subViolationId).FirstOrDefault().Name;
            var userID = User.Identity.GetUserId();
            if (createViolationViewModel.DynamicValue != null)
            {
                violation.HasDynamic = true;
                string[] stringArray = (string[])createViolationViewModel.DynamicValue;
                string dynamicValue = stringArray[0];
                violation.DynamicValue = dynamicValue;
                violation.DynamicName = createViolationViewModel.DynamicName;
            }
            else
            {
                violation.HasDynamic = false;
            }

            if (!string.IsNullOrEmpty(userID))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                violation.CreatorUser = currentUser;
            }

            context.Violations.Add(violation);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public ActionResult LoadSubViolations(string selectedMainV)
        {
            int mainId = int.Parse(selectedMainV);

            //List<ViolationSubType> violationSubTypesList = context.ViolationMainTypes.Where(u => u.Id == mainId).SelectMany(u => u.ViolationSubTypes).ToList();
            var tempSubTypes = context.ViolationMainTypes.Where(u => u.Id == mainId).SelectMany(u => u.ViolationSubTypes, (parent, child) => new { child.Name, child.Id });

            //var json = JsonConvert.SerializeObject(new { violationSubTypesList = violationSubTypesList });
            //return new JsonNetResult(violationSubTypesList);
            //dynamic dyn = JsonConvert.DeserializeObject(violationSubTypesList);
            return Json(tempSubTypes, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Details(int id)
        {
            Violation violation = context.Violations.Where(x => x.Id == id).FirstOrDefault();
            violation.ViewCount++;
            context.Violations.Attach(violation);
            context.Entry(violation).State = EntityState.Modified;
            context.SaveChanges();
            return View(violation);
        }
        public ActionResult ActiveViolationsGeneral()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetViolationsForMap()
        {

            var result = from d in context.Violations
                         select new
                         {
                             Latitude = d.Latitude,
                             Longitude = d.Longtitude,
                             Id = d.Id
                         };

            return Json(result);
        }
        public ActionResult Edit(int id)
        {
            Violation violation = context.Violations.Where(x => x.Id == id).FirstOrDefault();
            //CreateViolationViewModel createViolationViewModel = new CreateViolationViewModel();
            //createViolationViewModel.Address = violation.Address;
            //createViolationViewModel.Description = violation.Description;
            //createViolationViewModel.Latitude = violation.Latitude;
            //createViolationViewModel.Longitude = violation.Longtitude;
            //createViolationViewModel.MainViolationType = violation.MainType;
            //createViolationViewModel.Severity = violation.Severity;
            //createViolationViewModel.SubViolationType = violation.SubType;
            //createViolationViewModel.Title = violation.Title;
            //createViolationViewModel.UploadedPicPath = violation.ViolationPictures.FirstOrDefault().MediumPath;
            return View(violation);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Violation violation)
        {

            context.Violations.Attach(violation);
            context.Entry(violation).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Details", new { id = violation.Id });
        }
        public ActionResult RetrieveViolationDynamic(int selectedSubV)
        {
            int intTemp = 0;
            double doubleTemp = 0;
            ViolationDetail violationDetail = context.ViolationSubTypes.Where(x => x.Id == selectedSubV).Select(x => x.ViolationDetail).FirstOrDefault();
            if (violationDetail.PropertyType == PropertyType.Boolean)
                return new EmptyResult();
            else if (violationDetail.PropertyType == PropertyType.Integer)
            {
                return PartialView("ViolationDynamics", new ViolationDetailView() { DynamicName = violationDetail.PropertyName, DynamicValue = intTemp });
            }
            else
            {
                return PartialView("ViolationDynamics", new ViolationDetailView() { DynamicName = violationDetail.PropertyName, DynamicValue = doubleTemp });
            }
        }
        [HttpPost]
        public ActionResult GetNearViolations(string currentLat, string currentLong)
        {
            currentLat = currentLat.Replace(",", ".");
            currentLong = currentLong.Replace(",", ".");

            string origin = currentLat + "," + currentLong;
            List<ViolationDistance> violationDistances = new List<ViolationDistance>();
            var allViolations = from o in context.Violations
                                select new
                                {
                                    Id = o.Id,
                                    Latitude = o.Latitude,
                                    Longitude = o.Longtitude,
                                    SmallImagePath = o.ViolationPictures.FirstOrDefault().SmallPath
                                };
            foreach (var item in allViolations)
            {

                string destination = item.Latitude + "," + item.Longitude;
                // Build your request to the API
                var request = WebRequest.Create("https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origin + "&destinations=" + destination);
                // Indicate you are looking for a JSON response
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();

                // Read through the response
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    // Define a serializer to read your response
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    // Get your results
                    dynamic result = serializer.DeserializeObject(sr.ReadToEnd());

                    // Read the distance property from the JSON request
                    var distance = result["rows"][0]["elements"][0]["distance"]["text"]; // yields "1,300 KM" 
                    var duration = result["rows"][0]["elements"][0]["duration"]["text"];
                    var destinationAddress = result["destination_addresses"];
                    violationDistances.Add(new ViolationDistance()
                        {
                            Id = item.Id,
                            Distance = distance,
                            SmallPath = item.SmallImagePath,
                            CityName = "",
                            DistrictName = "",
                            Duration = duration
                        });
                }

            }
            //violationDistances=violationDistances.OrderBy(x=>x.Distance)
            return Json(violationDistances.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CalculateViolationSeverity(double enteredValue, int violationSubType)
        {
            string result="";
            int violationDetailId = context.ViolationSubTypes.Where(x => x.Id == violationSubType).FirstOrDefault().ViolationDetail.Id;
            ViolationDetail violationDetails = context.ViolationDetails.Where(x => x.Id == violationDetailId).FirstOrDefault();
            if (violationDetails.SeverityRelationType == SeverityRelationType.Ascending)
            {
                if (enteredValue >= violationDetails.LowSeverityValue && enteredValue<violationDetails.MediumSeverityValue)
                {
                    result = "You should choose Low Severity because the value was entered is in Low Severity Range : " + violationDetails.LowSeverityValue + "-" + (violationDetails.MediumSeverityValue);
                }
                else if (enteredValue >= violationDetails.MediumSeverityValue && enteredValue < violationDetails.HighSeverityValue)
                {
                    result = "You should choose Medium Severity because the value was entered is in Medium Severity Range : " + violationDetails.MediumSeverityValue + "-" + (violationDetails.HighSeverityValue);
                }
                else if (enteredValue >= violationDetails.HighSeverityValue && enteredValue < violationDetails.CatastrophicSeverityValue)
                {
                    result = "You should choose High Severity because the value was entered is in High Severity Range : " + violationDetails.HighSeverityValue + "-" + (violationDetails.CatastrophicSeverityValue);

                }
                else if (enteredValue >= violationDetails.CatastrophicSeverityValue)
                {
                    result = "You should choose Catastrophic Severity because the value was entered is in Catastrophic Severity Range : " + violationDetails.CatastrophicSeverityValue + "-∞";
                }
                else
                {
                    result = "It doesn't seem as a violation because the was entered below Low Severity Range : ";

                }
            }
            if (violationDetails.SeverityRelationType == SeverityRelationType.Descending)
            {
                if (enteredValue <= violationDetails.CatastrophicSeverityValue)
                {
                    result = "You should choose Catastrophic Severity because the value was entered is in Catastrophic Severity Range : " + violationDetails.CatastrophicSeverityValue + "-(-∞)";
                }
                else if (enteredValue <= violationDetails.HighSeverityValue && enteredValue > violationDetails.CatastrophicSeverityValue)
                {
                    result = "You should choose High Severity because the value was entered is in High Severity Range : " + violationDetails.HighSeverityValue + "-" + (violationDetails.CatastrophicSeverityValue);
                }
                else if (enteredValue <= violationDetails.MediumSeverityValue && enteredValue > violationDetails.HighSeverityValue)
                {
                    result = "You should choose Medium Severity because the value was entered is in Medium Severity Range : " + violationDetails.MediumSeverityValue + "-" + (violationDetails.HighSeverityValue );
                }
                else if(enteredValue<=violationDetails.LowSeverityValue && enteredValue>violationDetails.MediumSeverityValue)
                {
                    result = "You should choose Low Severity because the value was entered is in Low Severity Range : " + (violationDetails.LowSeverityValue)+"-"+(violationDetails.MediumSeverityValue);

                }
                else
                {
                    result = "It doesn't seem as a violation because the was entered above Low Severity Range :  "+(violationDetails.LowSeverityValue);

                }

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ViolationAdminControl()
        {
            return View(context.Violations.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Violation violation = context.Violations.Where(x => x.Id == id).FirstOrDefault();
            foreach (var item in violation.ViolationPictures.ToList())
            {
                context.ViolationPictures.Remove(item);
            }
            foreach (var item in violation.Comments.ToList())
            {
                context.Comments.Remove(item);
            }
            context.Violations.Remove(violation);
            context.SaveChanges();
            return RedirectToAction("ViolationAdminControl", "Violation");
        }

        //    public ActionResult Edit(int id)
        //    {
        //        return View(context.Violations.Where(x=>x.Id==id).FirstOrDefault());
        //    }
        //    [HttpPost]
        //    public ActionResult Edit(Violation violation)
        //    {
        //        violation.LastEditorId=
        //    }
    }
}
