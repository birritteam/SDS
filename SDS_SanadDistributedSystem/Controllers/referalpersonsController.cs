using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SDS_SanadDistributedSystem.Models;
using SDS_SanadDistributedSystem.viewModel;
using Microsoft.AspNet.Identity;
using SDS_SanadDistributedSystem.Hubs;
using Microsoft.AspNet.SignalR;
using System.Collections;
using System.Globalization;

namespace SDS_SanadDistributedSystem.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "superadmin,admin,cmEducation,cmProfessional,cmChildProtection,cmPsychologicalSupport1,cmPsychologicalSupport2,cmPsychologicalSupport3,cmDayCare,cmHomeCare,cmAwareness,cmSGBV,cmSmallProjects ,cmIOutReachTeam,cmInkindAssistance,receptionist")]

    public class referalpersonsController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        private static List<referalperson> referalpersonlist = new List<referalperson>();
        private static List<referalPersonViewModel> referalpersonlistviewmodel = new List<referalPersonViewModel>();
        // GET: referalpersons
        public async Task<ActionResult> Index()
        {
            //if user in role then retuen row by role
            //by role (case manager) type  we return referal's people
            //cmEducation
            //cmProfessional
            //cmChildProtection
            //cmPsychologicalSupport1
            //cmPsychologicalSupport2
            //cmPsychologicalSupport3
            //cmDayCare
            //cmHomeCare
            //cmSGBV
            //cmSmallProjects
            //cmIOutReachTeam
            //cmInkindAssistance المساعدات العينية
            //return first 100 row order by submit  date

            //يتم الفلترة بعد تحديد المستخدم ثم الحصول على اسم الرول ثم الحصول  على  مجدول الخدمات على  الخدمات المرتبة بهذه الرول
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
            var roles = user.AspNetRoles.ToList();
            if (roles.Count > 0)
            {
               
                //  roles.First().Name
                if (User.IsInRole("cmEducation"))
                {  //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmEducation").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK ==role_id && r.idcenter_FK ==user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "تعليمي";
                }
                else if (User.IsInRole("cmProfessional"))
                {
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmProfessional").First().Id;
                    ViewBag.role_id = role_id;
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "مهني";
                }
                else if (User.IsInRole("cmChildProtection"))
                {
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmChildProtection").First().Id;
                    ViewBag.role_id = role_id;
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "حماية الصفل";
                }
                else if (User.IsInRole("cmPsychologicalSupport1"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport1").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "1الدعم النفسي";
                }
                else if (User.IsInRole("cmPsychologicalSupport2"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport2").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "الدعم النفسي2";
                }
                else if (User.IsInRole("cmPsychologicalSupport3"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport3").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "3الدعم النفسي";
                }
                else if (User.IsInRole("cmDayCare"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmDayCare").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "رعاية نهارية";
                }
                else if (User.IsInRole("cmHomeCare"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmHomeCare").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "رعاية منزلية";
                }
                else if (User.IsInRole("cmAwareness"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmAwareness").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK ).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "التوعية ";
                }
                else if (User.IsInRole("cmSmallProjects"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmSmallProjects").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "مشاريع صغيرة";
                }
                else if (User.IsInRole("cmIOutReachTeam"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmIOutReachTeam").First().Id;
                    ViewBag.role_id = role_id;
                    //  referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK ==user.idcenter_FK).OrderBy(r => r.submittingdate).Take(100);
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service ).Where(r => r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(500);
                    ViewBag.temporal = db.temporals.ToList();
                    ViewBag.case_manager = "فريق وصول";
                }
                else if (User.IsInRole("cmInkindAssistance"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmInkindAssistance").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Month).OrderByDescending(r => r.submittingdate.Value.Day).Take(100);
                    ViewBag.case_manager = "المساعدات العينية";
                }
            }

            //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
            return View(await referalpersons.ToListAsync());
        }

        // this get new referalperson when added to database .. and when user click on جرس

        public JsonResult GetNotificationsReferal()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            string usertosend = db.AspNetUsers.SingleOrDefault(u => u.UserName == User.Identity.Name).Id;
            var list = NC.GetReferals(notificationRegisterTime, usertosend);

            Session["LastUpdated"] = DateTime.Now;
            return new JsonResult { Data = list.Select(r => new { idreferalperson = r.idreferalperson, idperson_FK = r.idperson_FK, idcase_FK = r.idcase_FK, personname = r.person.fname + " " + r.person.lname, serviceType = r.service.name }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult FillServices(string caseId)
        {
            // List<service> servicedata = new List<service>();
            int id = Int32.Parse(caseId);

            List<service> services_case = db.services.Where(c => c.idcase_FK == id).OrderByDescending(ser => ser.idservice).ToList();
            List<serviceViewModel> jsonservices = new List<serviceViewModel>();
            for (int i=0;i< services_case.Count;i++)
            {

                service s = services_case.ElementAt(i);
                serviceViewModel ss = new serviceViewModel();
                ss.idservice = s.idservice;
                ss.name = s.name;
               
                   
                    List<UserViewModel> user_list = new List<UserViewModel>();
                    List < AspNetUser> recivers_users = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == s.idrole_FK)).ToList();

                    foreach (var u in recivers_users)
                    { UserViewModel uvm = new UserViewModel();
                        uvm.Id = u.Id;
                        uvm.Email = u.Email;
                        uvm.UserName = u.UserName;

                        user_list.Add(uvm);
                     }
                    //في حال لايوجد مستقبلين للخدم وهذا مستحيل 
               if(recivers_users.Count==0)
                {
                    UserViewModel uvm = new UserViewModel();
                    uvm.Id ="0";
                    uvm.Email ="لايوجد مستقبِل" ;
                    uvm.UserName = "لايوجد مستقبِل";
                    user_list.Add(uvm);

                }

                    ss.recivers = user_list;
                

                jsonservices.Add(ss);
            }

            if (services_case.Count == 0)
            {
                serviceViewModel ss = new serviceViewModel();
                ss.idservice =0;
                ss.name = "لايوجد خدمات مفّعلة";

                UserViewModel uvm = new UserViewModel();
                uvm.Id = "0";
                uvm.Email = "لايوجد مستقبِل";
                uvm.UserName = "لايوجد مستقبِل";
                List<UserViewModel> user_list = new List<UserViewModel>();
                user_list.Add(uvm);

                ss.recivers = user_list;
                jsonservices.Add(ss);
            }
                
            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //string result = javaScriptSerializer.Serialize(jsonservices);
            return Json(jsonservices, JsonRequestBehavior.AllowGet);
        }
        

      public ActionResult FillRecivers(string serviceId)
        {
           
            int id = Int32.Parse(serviceId);

                service s = db.services.Find(id);
               
                List<UserViewModel> user_list = new List<UserViewModel>();
                List<AspNetUser> recivers_users = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == s.idrole_FK)).ToList();
                foreach (var u in recivers_users)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.Id = u.Id;
                    uvm.Email = u.Email;
                    uvm.UserName = u.UserName;

                    user_list.Add(uvm);
                }

            if (recivers_users.Count == 0)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.Id = "0";
                uvm.Email = "لايوجد مستقبِل";
                uvm.UserName = "لايوجد مستقبِل";
                user_list.Add(uvm);

            }

            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //string result = javaScriptSerializer.Serialize(jsonservices);
            return Json(user_list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchReferalByName(string name,string idrole)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<referalperson> referalpersons = db.referalpersons.Where(r=>(r.person.fname == name || r.person.lname == name || r.person.fname+" "+r.person.lname == name) &&  r.service.idrole_FK == idrole && r.idcenter_FK == user.idcenter_FK).ToList();
            List<RPSearchViewModel> RPSearchViewModels=new List<viewModel.RPSearchViewModel>();

            foreach(referalperson r in referalpersons)
            {
                RPSearchViewModel rps = new RPSearchViewModel();

                rps.idcase = r.idcase_FK.ToString();
                rps.idperson = r.idperson_FK;
                rps.idreferalperson = r.idreferalperson.ToString();
                rps.name = r.person.fname+" "+r.person.lname;
                rps.submittingdate = r.submittingdate.Value.ToShortDateString();
                if (r.referaldate!=null)
                rps.referaldate= r.referaldate.Value.ToShortDateString();
               else
                    rps.referaldate = "لم يحدد التاريخ";

                if (r.servicestate== "Pending" && r.referalstate== "Pending")
                rps.type = "جديد";
                else if (r.servicestate == "Pending" && r.referalstate == "Approved")
                    rps.type = "مقبولة";
                else if (r.servicestate == "Pending" && r.referalstate == "OutReach")
                    rps.type = " مقبولة-وصول";
                else if (r.servicestate == "Pending" && r.referalstate == "Rejected")
                    rps.type = "مرفوضة";
                else if (r.servicestate == "Pending" && r.referalstate == "External")
                    rps.type = "خارجي";
                else if (r.servicestate == "In prgress" && r.referalstate == "Approved")
                    rps.type = "قيد المتابعة";
                else if (r.servicestate == "In prgress" && r.referalstate == "OutReach")
                    rps.type = "قيد المتابعة-وصول";
                else if (r.servicestate == "Closed" && r.referalstate == "Approved")
                    rps.type = "مغلقة";
                else if (r.servicestate == "Closed" && r.referalstate == "OutReach")
                    rps.type = "مغلقة-وصوب";


                if (r.servicestartdate != null)
                    rps.servicestartdate = r.servicestartdate.Value.ToShortDateString();
                else
                    rps.servicestartdate = "لم يحدد التاريخ";

                if (r.serviceenddate != null)
                    rps.serviceenddate = r.serviceenddate.Value.ToShortDateString();
                else
                    rps.serviceenddate = "لم يحدد التاريخ";

                if (r.senderevalution != null)
                    rps.senderevalution = r.senderevalution;
                else
                    rps.senderevalution = "لم يحدد ";

                if (r.recieverevalution != null)
                    rps.recieverevalution = r.recieverevalution;
                else
                    rps.recieverevalution = "لم يحدد ";

                if (r.outreachnote != null)
                    rps.outreachnote = r.outreachnote;
                else
                    rps.outreachnote = "لم يحدد ";

                

                RPSearchViewModels.Add(rps);
            }

            return Json(RPSearchViewModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchReferalByDate( string from, string to, string idrole)
        {
            DateTime from_date = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            DateTime to_date = DateTime.ParseExact(to, "yyyy-MM-dd", null);
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            List <referalperson> referalpersons = db.referalpersons.Where(r => r.submittingdate > from_date && r.submittingdate < to_date && r.service.idrole_FK == idrole && r.idcenter_FK == user.idcenter_FK).ToList();
            List<RPSearchViewModel> RPSearchViewModels = new List<viewModel.RPSearchViewModel>();

            foreach (referalperson r in referalpersons)
            {
                RPSearchViewModel rps = new RPSearchViewModel();

                rps.idcase = r.idcase_FK.ToString();
                rps.idperson = r.idperson_FK;
                rps.idreferalperson = r.idreferalperson.ToString();
                rps.name = r.person.fname + " " + r.person.lname;
                rps.submittingdate = r.submittingdate.Value.ToShortDateString();
                if (r.referaldate != null)
                    rps.referaldate = r.referaldate.Value.ToShortDateString();
                else
                    rps.referaldate = "لم يحدد التاريخ";

                if (r.servicestate == "Pending" && r.referalstate == "Pending")
                    rps.type = "جديد";
                else if (r.servicestate == "Pending" && r.referalstate == "Approved")
                    rps.type = "مقبولة";
                else if (r.servicestate == "Pending" && r.referalstate == "OutReach")
                    rps.type = " مقبولة-وصول";
                else if (r.servicestate == "Pending" && r.referalstate == "Rejected")
                    rps.type = "مرفوضة";
                else if (r.servicestate == "Pending" && r.referalstate == "External")
                    rps.type = "خارجي";
                else if (r.servicestate == "In prgress" && r.referalstate == "Approved")
                    rps.type = "قيد المتابعة";
                else if (r.servicestate == "In prgress" && r.referalstate == "OutReach")
                    rps.type = "قيد المتابعة-وصول";
                else if (r.servicestate == "Closed" && r.referalstate == "Approved")
                    rps.type = "مغلقة";
                else if (r.servicestate == "Closed" && r.referalstate == "OutReach")
                    rps.type = "مغلقة-وصوب";


                if (r.servicestartdate != null)
                    rps.servicestartdate = r.servicestartdate.Value.ToShortDateString();
                else
                    rps.servicestartdate = "لم يحدد التاريخ";

                if (r.serviceenddate != null)
                    rps.serviceenddate = r.serviceenddate.Value.ToShortDateString();
                else
                    rps.serviceenddate = "لم يحدد التاريخ";

                if (r.senderevalution != null)
                    rps.senderevalution = r.senderevalution;
                else
                    rps.senderevalution = "لم يحدد ";

                if (r.recieverevalution != null)
                    rps.recieverevalution = r.recieverevalution;
                else
                    rps.recieverevalution = "لم يحدد ";

                if (r.outreachnote != null)
                    rps.outreachnote = r.outreachnote;
                else
                    rps.outreachnote = "لم يحدد ";



                RPSearchViewModels.Add(rps);
            }

            return Json(RPSearchViewModels, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------delete personReferal-----------
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult personReferal([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,servicestate,referalsender_FK,senderevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        //{
        //    referalpersonlist.Add(referalperson);

        //    referalPersonViewModel r = new referalPersonViewModel();
        //    r.idcase_FK = db.cases.Find( referalperson.idcase_FK).name;
        //    r.idcenter_FK = referalperson.idcenter_FK;
        //    r.idperson_FK = referalperson.idperson_FK;

        //    r.idservice_FK = db.services.Find(referalperson.idservice_FK).name;
        //    r.referalsender_FK = db.AspNetUsers.Find(User.Identity.GetUserId()).UserName;

        //    r.outreachnote = referalperson.outreachnote;

        //    r.referaldate = referalperson.referaldate;
        //    r.referalstate = referalperson.referalstate;

        //    r.servicestartdate = referalperson.servicestartdate;
        //    r.serviceenddate = referalperson.serviceenddate;
        //    r.servicestate = referalperson.servicestate;

        //    r.senderevalution = referalperson.senderevalution;
        //    r.recieverevalution = referalperson.recieverevalution;
        //    r.submittingdate = referalperson.submittingdate;

        //    referalpersonlistviewmodel.Add(r);

        //    return new JsonResult { Data= referalpersonlistviewmodel };
        //}

        //public ActionResult deleteRow(int selectedId)
        //{
        //   // int index = Int32.Parse(selectedId);
        //    referalpersonlist.RemoveAt(selectedId-1);
        //    referalpersonlistviewmodel.RemoveAt(selectedId - 1);

        //    return Json(referalpersonlistviewmodel, JsonRequestBehavior.AllowGet);
        //}

        [System.Web.Mvc.Authorize(Roles = "receptionist,cmSGBV")]
        //add new referal by Receptionist
        public ActionResult personReferal(string id)
        {         
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            referalperson rp = new referalperson();
            
            person per = db.people.Where(p => p.idperson == id).First();
            rp.person=per;

            ViewBag.idperson_FK = new SelectList(db.people.Where(p => p.idperson == id), "idperson", "fname", id);

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(u => u.UserName == user.UserName), "Id", "UserName");


            var firstcaseid = db.cases.First().idcase;
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", firstcaseid);

            var first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled).First();
      

            ViewBag.services = new SelectList(db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled), "idservice", "name", first_service_firstcase.name);

            ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == first_service_firstcase.idrole_FK) && u.idcenter_FK == user.idcenter_FK), "Id", "UserName");


            ViewBag.idcenter_FK = new SelectList(db.centers.Where(c => c.idcenter == user.idcenter_FK), "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");


            //0------------------------------------------------------------------------------------00000000000000000000000000000000000000000
            //ViewBag.cmEducation = new SelectList(db.services.Where(s => s.idcase_FK == 1), "idservice", "name");
            //ViewBag.cmProfessional = new SelectList(db.services.Where(s => s.idcase_FK == 2), "idservice", "name");
            //ViewBag.cmChildProtection = new SelectList(db.services.Where(s => s.idcase_FK == 3), "idservice", "name");

            //ViewBag.cmPsychologicalSupport = new SelectList(db.services.Where(s => s.idcase_FK == 4), "idservice", "name");
            //ViewBag.cmPsychologicalSupport2 = new SelectList(db.services.Where(s => s.idcase_FK == 5), "idservice", "name");
            //ViewBag.cmPsychologicalSupport3 = new SelectList(db.services.Where(s => s.idcase_FK == 6), "idservice", "name");

            //ViewBag.cmDayCare = new SelectList(db.services.Where(s => s.idcase_FK == 6), "idservice", "name");
            //ViewBag.cmHomeCare = new SelectList(db.services.Where(s => s.idcase_FK == 7), "idservice", "name");

            //ViewBag.cmSGBV = new SelectList(db.services.Where(s => s.idcase_FK == 8), "idservice", "name");
            //ViewBag.cmSmallProjects = new SelectList(db.services.Where(s => s.idcase_FK == 9), "idservice", "name");
            //ViewBag.cmIOutReachTeam = new SelectList(db.services.Where(s => s.idcase_FK == 10), "idservice", "name");

            //ViewBag.cmInkindAssistance = new SelectList(db.services.Where(s => s.idcase_FK == 11), "idservice", "name");

            //0------------------------------------------------------------------------------------00000000000000000000000000000000000000000


            return View(rp);
        }
        //add new referal by CaseManager
        public ActionResult personReferalByCaseManager(string id, int idcase)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            referalperson rp = new referalperson();

            person per = db.people.Where(p => p.idperson == id).First();
            rp.person = per;

            ViewBag.idperson_FK = new SelectList(db.people.Where(p => p.idperson == id), "idperson", "fname", id);

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(u => u.UserName == user.UserName), "Id", "UserName");


            var firstcaseid = db.cases.Where(r => r.idcase != idcase).First().idcase;
            ViewBag.idcase_FK = new SelectList(db.cases.Where(r => r.idcase != idcase), "idcase", "name", firstcaseid);

            var first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid).First();


            ViewBag.services = new SelectList(db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled), "idservice", "name", first_service_firstcase.name);

            ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == first_service_firstcase.idrole_FK ) && u.idcenter_FK==user.idcenter_FK), "Id", "UserName");


            ViewBag.idcenter_FK = new SelectList(db.centers.Where(c => c.idcenter == user.idcenter_FK), "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");


            //0------------------------------------------------------------------------------------00000000000000000000000000000000000000000
            //ViewBag.cmEducation = new SelectList(db.services.Where(s => s.idcase_FK == 1), "idservice", "name");
            //ViewBag.cmProfessional = new SelectList(db.services.Where(s => s.idcase_FK == 2), "idservice", "name");
            //ViewBag.cmChildProtection = new SelectList(db.services.Where(s => s.idcase_FK == 3), "idservice", "name");

            //ViewBag.cmPsychologicalSupport = new SelectList(db.services.Where(s => s.idcase_FK == 4), "idservice", "name");
            //ViewBag.cmPsychologicalSupport2 = new SelectList(db.services.Where(s => s.idcase_FK == 5), "idservice", "name");
            //ViewBag.cmPsychologicalSupport3 = new SelectList(db.services.Where(s => s.idcase_FK == 6), "idservice", "name");

            //ViewBag.cmDayCare = new SelectList(db.services.Where(s => s.idcase_FK == 6), "idservice", "name");
            //ViewBag.cmHomeCare = new SelectList(db.services.Where(s => s.idcase_FK == 7), "idservice", "name");

            //ViewBag.cmSGBV = new SelectList(db.services.Where(s => s.idcase_FK == 8), "idservice", "name");
            //ViewBag.cmSmallProjects = new SelectList(db.services.Where(s => s.idcase_FK == 9), "idservice", "name");
            //ViewBag.cmIOutReachTeam = new SelectList(db.services.Where(s => s.idcase_FK == 10), "idservice", "name");

            //ViewBag.cmInkindAssistance = new SelectList(db.services.Where(s => s.idcase_FK == 11), "idservice", "name");

            //0------------------------------------------------------------------------------------00000000000000000000000000000000000000000

              return View(rp);
        }
       // [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> sendReferals(List<referalPersonViewModel> referals)
        {
            try
            {
                foreach (referalPersonViewModel rpvm in referals)
                {
                    string email = "amro@gmail.com"; 

                    referalperson rp = new referalperson();
                    rp.idperson_FK = rpvm.idperson_FK;

                    rp.idcase_FK = Int32.Parse(rpvm.idcase_FK);

                    rp.idservice_FK = Int32.Parse(rpvm.idservice_FK);

                    rp.submittingdate = DateTime.Now;

                    //rp.referalstate = rpvm.referalstate;

                    //rp.servicestate = rpvm.servicestate;

                    rp.referalstate = "Pending";

                    rp.servicestate = "Pending";

                    rp.referalreicver_FK = /*"84c2c89c-a3a2-4964-80f5-aaafa33c1d67";*/  rpvm.referalreciever_FK;
                   // rp.referalreicver_FK = db.AspNetUsers.SingleOrDefault(user => user.Email == rpvm.referalreciever_FK).Id;

                    rp.senderevalution = rpvm.senderevalution;

                    rp.idcenter_FK = db.AspNetUsers.Find(User.Identity.GetUserId()).center.idcenter;
                    rp.referalsender_FK = User.Identity.GetUserId();

                    rp.outreachnote = rpvm.outreachnote;


                    db.referalpersons.Add(rp);
                    await db.SaveChangesAsync();

                    // send real time notifcation to reciver user
                    var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    string username = db.AspNetUsers.SingleOrDefault(user => user.Id == rp.referalreicver_FK).UserName;
                    notificationHub.Clients.All.notify("added", username);
                }
                return new JsonResult { Data = "Success" };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "fail send referals" };
            }
        }


        // GET: referalpersons/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            referalperson referalperson = await db.referalpersons.FindAsync(id);
            if (referalperson == null)
            {
                return HttpNotFound();
            }
            return View(referalperson);
        }

        // GET: referalpersons/Create
        public ActionResult Create()
        {
            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name");
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");
            return View();
        }

        // POST: referalpersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                db.referalpersons.Add(referalperson);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email", referalperson.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalperson.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", referalperson.idcenter_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", referalperson.idperson_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", referalperson.idservice_FK);
            return View(referalperson);
        }

        // GET: referalpersons/Edit/5
        public async Task<ActionResult> Edit(int? idreferalperson, string idperson, int? idcase)
        {
            if (idreferalperson == null || idperson == null || idcase == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            referalperson referalperson = await db.referalpersons.FindAsync(idreferalperson, idperson, idcase);
            if (referalperson == null)
            {
                return HttpNotFound();
            }
                List<string> serviceStates = new List<string>();
                serviceStates.Add("Pending");
                serviceStates.Add("In prgress");
                serviceStates.Add("Closed");

                List<string> referalstates = new List<string>();
                referalstates.Add("Pending");
                referalstates.Add("Approved");
                referalstates.Add("Rejected");
                referalstates.Add("OutReach");
                referalstates.Add("External");

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(r => r.Id == referalperson.referalsender_FK), "Id", "Email", referalperson.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalperson.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers.Where(r => r.idcenter == referalperson.idcenter_FK), "idcenter", "name", referalperson.idcenter_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", referalperson.idperson_FK);
            ViewBag.idservice_FK = new SelectList(db.services.Where(s => s.idcase_FK == idcase), "idservice", "name", referalperson.idservice_FK);

            ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == referalperson.service.idrole_FK)), "Id", "UserName");

            ViewBag.referalstate = new SelectList(referalstates, referalperson.referalstate);
            ViewBag.servicestate = new SelectList(serviceStates, referalperson.servicestate);

            return View(referalperson);
        }

        // POST: referalpersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referalperson).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email", referalperson.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalperson.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", referalperson.idcenter_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", referalperson.idperson_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", referalperson.idservice_FK);
            return View(referalperson);
        }

        // GET: referalpersons/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            referalperson referalperson = await db.referalpersons.FindAsync(id);
            if (referalperson == null)
            {
                return HttpNotFound();
            }
            return View(referalperson);
        }

        // POST: referalpersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            referalperson referalperson = await db.referalpersons.FindAsync(id);
            db.referalpersons.Remove(referalperson);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
