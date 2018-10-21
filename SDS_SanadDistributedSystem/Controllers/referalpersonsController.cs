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
    [System.Web.Mvc.Authorize(Roles = "superadmin,admin,cmEducation,cmProfessional,cmChildProtection,cmPsychologicalSupport1,cmPsychologicalSupport2,cmPsychologicalSupport3,cmDayCare,cmHomeCare,cmAwareness,cmSGBV,cmSmallProjects ,cmIOutReachTeam,cmInkindAssistance,receptionist,"
        + "coEducation,coProfessional,coChildProtection,coPsychologicalSupport,coDayCare,coHomeCare,coAwareness,coSGBV,coSmallProjects,coOutReachTeam,coInkindAssistance")]

    public class referalpersonsController : BaseController
    {
        private sds_dbEntities db = new sds_dbEntities();
        private static List<referalperson> referalpersonlist = new List<referalperson>();
        private static List<referalPersonViewModel> referalpersonlistviewmodel = new List<referalPersonViewModel>();

        [System.Web.Mvc.Authorize(Roles = "superadmin,cmEducation,cmProfessional,cmChildProtection,cmPsychologicalSupport1,cmPsychologicalSupport2,cmPsychologicalSupport3,cmDayCare,cmHomeCare,cmAwareness,cmSmallProjects ,cmInkindAssistance")]
        // GET: referalpersons
        public async Task<ActionResult> Index()
        {
            //if user in role then retuen row by role
            //by role (case manager) type  we return referal's people
            //----------------------------------------------------مدراء الحالة------------
            //cmEducation
            //cmProfessional
            //cmChildProtection
            //cmPsychologicalSupport1
            //cmPsychologicalSupport2
            //cmPsychologicalSupport3
            //cmDayCare
            //cmHomeCare
            //cmSGBV<<<<<<<<<<<<<<<<<<<<<لايوجد اطلاع على الاحالات
            //cmSmallProjects
            //cmIOutReachTeam
            //cmInkindAssistance المساعدات العينية

            //return first 100 row order by submit  date
            //يتم الفلترة بعد تحديد المستخدم ثم الحصول على اسم الرول ثم الحصول  على  مجدول الخدمات على  الخدمات المرتبة بهذه الرول



            // جلب عدد الحالات التي تم إرسالها إلى مدير الحالة المسجل دخول حاليا والتي كان فيها أوف لاين
            //BaseController.CountNotificationOffilicn = db.referalpersons.Where(rp => rp.servicestate == "Pending" && rp.referalstate == "Pending" && rp.AspNetUser1.UserName == user.UserName).Count();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            ViewBag.CountNotficationOffline = db.referalpersons.Where(rp => rp.servicestate == "Pending" && rp.referalstate == "Pending" && rp.AspNetUser1.UserName == user.UserName).Count();






            var referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
            var roles = user.AspNetRoles.ToList();
            if (roles.Count > 0)
            {

                //  roles.First().Name
                if (User.IsInRole("cmEducation"))
                {  //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmEducation").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "تعليمي";
                }
                else if (User.IsInRole("cmProfessional"))
                {
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmProfessional").First().Id;
                    ViewBag.role_id = role_id;
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "مهني";
                }
                else if (User.IsInRole("cmChildProtection"))
                {
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmChildProtection").First().Id;
                    ViewBag.role_id = role_id;
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "حماية الطفل";
                }
                else if (User.IsInRole("cmPsychologicalSupport1"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport1").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "الدعم النفسي-مرشد نفسي)";
                }
                else if (User.IsInRole("cmPsychologicalSupport2"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport2").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "الدعم النفسي -مسؤول تأهيل منزلي";
                }
                else if (User.IsInRole("cmPsychologicalSupport3"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport3").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "الدعم النفسي -مسؤول أنشطة";
                }
                else if (User.IsInRole("cmDayCare"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmDayCare").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "الرعاية نهارية";
                }
                else if (User.IsInRole("cmHomeCare"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmHomeCare").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "الرعاية منزلية";
                }
                else if (User.IsInRole("cmAwareness"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmAwareness").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "التوعية ";
                }
                else if (User.IsInRole("cmSmallProjects"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmSmallProjects").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "المشاريع صغيرة";
                }
                //else if (User.IsInRole("cmIOutReachTeam"))
                //{
                //    //return first 100 row order by submit  date
                //    var role_id = db.AspNetRoles.Where(r => r.Name == "cmSGBV").First().Id;
                //    ViewBag.role_id = db.AspNetRoles.Where(r => r.Name == "cmIOutReachTeam").First().Id;
                //    //  referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK ==user.idcenter_FK).OrderBy(r => r.submittingdate);
                //    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK != role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                //    ViewBag.temporal = db.temporals.Where(t => t.idcenter_FK == user.idcenter_FK).ToList();
                //    ViewBag.case_manager = "فريق وصول";
                //}
                else if (User.IsInRole("cmInkindAssistance"))
                {
                    //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == "cmInkindAssistance").First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "المساعدات العينية";
                }
            }

            //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
            return View(await referalpersons.ToListAsync());
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> PendingReReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                // rp.serviceenddate = DateTime.Now;
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = null;
                rp.serviceenddate = null;
                rp.referalstate = "Pending";
                rp.servicestate = "Pending";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> PendingApprovedReReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = null;
                rp.serviceenddate = null;

                rp.referalstate = "Approved";
                rp.servicestate = "Pending";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> PendingOutReachReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = null;
                rp.serviceenddate = null;

                rp.referalstate = "OutReach";
                rp.servicestate = "Pending";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> PendingRejectedReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = null;
                rp.serviceenddate = null;
                rp.referalstate = "Rejected";
                rp.servicestate = "Pending";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> PendingExternalReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = null;
                rp.serviceenddate = null;
                rp.referalstate = "External";
                rp.servicestate = "Pending";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApprovedInprgressReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = DateTime.Now;
                rp.serviceenddate = null;
                rp.referalstate = "Approved";
                rp.servicestate = "In prgress";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> OutReachInprgressReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.referaldate = DateTime.Now;
                rp.servicestartdate = DateTime.Now;
                rp.serviceenddate = null;
                rp.referalstate = "OutReach";
                rp.servicestate = "In prgress";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);

                rp.referaldate = DateTime.Now;
                if (rp.servicestartdate == null)
                    rp.servicestartdate = DateTime.Now;

                rp.serviceenddate = DateTime.Now;

                rp.referalstate = "Approved";
                rp.servicestate = "Closed";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> OutReachClosedReReferal(int idreferalperson, int idperson, int idcase)
        {//,string idfrom,string idto,int rowid
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);

                rp.referaldate = DateTime.Now;
                rp.serviceenddate = DateTime.Now;

                rp.referalstate = "OutReach";
                rp.servicestate = "Closed";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                RPSearchViewModel rps = getRP(rp);
                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        rps
                        //   Data = "sucsses"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
        }

        public RPSearchViewModel getRP(referalperson rp)
        {
            RPSearchViewModel rps = new RPSearchViewModel();

            rps.idcase = rp.idcase_FK.ToString();
            rps.idperson = rp.idperson_FK;
            rps.idreferalperson = rp.idreferalperson.ToString();
            rps.name = rp.person.fname + " " + rp.person.lname;
            rps.submittingdate = rp.submittingdate.Value.ToShortDateString();

            if (rp.person.gender != null)
                rps.gender = rp.person.gender;
            else
                rps.gender = "لم يحدد";

             if (rp.person.age != null)
                rps.age = rp.person.age.ToString() ;
            else
                rps.age ="لم يحدد";

            rps.phone1 = rp.person.phone1;
            rps.phone2 = rp.person.phone2;

            if (rp.person.family.phone1 != null)
                rps.family_phone1 = rp.person.family.phone1 + " (" + rp.person.family.phone1owner + ") ";
            else
                rps.family_phone1 = rp.person.family.phone1;

            if (rp.person.family.phone2 != null)
                rps.family_phone2 = rp.person.family.phone2 + " (" + rp.person.family.phone2owner + ") ";
            else
                rps.family_phone2 = rp.person.family.phone2;

            if (rp.referaldate != null)
                rps.referaldate = rp.referaldate.Value.ToShortDateString();
            else
                rps.referaldate = "لم يحدد التاريخ";

            rps.servicetype = rp.service.name;

            if (rp.servicestartdate != null)
                rps.servicestartdate = rp.servicestartdate.Value.ToShortDateString();
            else
                rps.servicestartdate = "لم يحدد التاريخ";

            if (rp.serviceenddate != null)
                rps.serviceenddate = rp.serviceenddate.Value.ToShortDateString();
            else
                rps.serviceenddate = "لم يحدد التاريخ";

            if (rp.senderevalution != null)
                rps.senderevalution = rp.senderevalution;
            else
                rps.senderevalution = "لم يحدد ";

            if (rp.recieverevalution != null)
                rps.recieverevalution = rp.recieverevalution;
            else
                rps.recieverevalution = "لم يحدد ";

            if (rp.outreachnote != null)
                rps.outreachnote = rp.outreachnote;
            else
                rps.outreachnote = "لم يحدد ";

            rps.personEevalution = rp.person.evaluation;
            rps.familyEvalution = rp.person.family.evaluation;

            rps.referalsender_FK = db.AspNetUsers.Find(rp.referalsender_FK).Email;

            return rps;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> closeSecretReferal(int idreferalperson, int idperson, int idcase)
        {
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.serviceenddate = DateTime.Now;

                rp.referalstate = "Approved";
                rp.servicestate = "Closed";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();


                //  return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        idperson = idperson,
                        servicestate = "مغلقة"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> openSecretReferal(int idreferalperson, int idperson, int idcase)
        {
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.serviceenddate = null;
                rp.referalstate = "Approved";
                rp.servicestate = "In prgress";
                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();
                //return new JsonResult { Data = "success" };

                return new JsonResult
                {
                    Data = new
                    {
                        idperson = idperson,
                        servicestate = "قيد المتابعة"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }


        }
        [System.Web.Mvc.Authorize(Roles = "superadmin,cmEducation,cmProfessional,cmChildProtection,cmPsychologicalSupport1,cmPsychologicalSupport2,cmPsychologicalSupport3,cmDayCare,cmHomeCare,cmAwareness,cmSmallProjects ,cmInkindAssistance,cmSGBV")]
        public async Task<ActionResult> IndexByRN(string rm)
        {


            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var referalpersons = (IQueryable<referalperson>)null;
            var roles = user.AspNetRoles.ToList();
            if (roles.Count > 0)
            {

                //  roles.First().Name
                if (User.IsInRole(rm))
                {  //return first 100 row order by submit  date
                    var role_id = db.AspNetRoles.Where(r => r.Name == rm).First().Id;
                    ViewBag.role_id = role_id;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id &&  r.referalreicver_FK == user.Id).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    if (rm.Equals("cmEducation"))
                        ViewBag.case_manager = "تعليمي";
                    if (rm.Equals("cmProfessional"))
                        ViewBag.case_manager = "مهني";
                    if (rm.Equals("cmChildProtection"))
                        ViewBag.case_manager = "حماية الطفل";
                    if (rm.Equals("cmPsychologicalSupport1"))
                        ViewBag.case_manager = "الدعم النفسي-مرشد نفسي";
                    if (rm.Equals("cmPsychologicalSupport2"))
                        ViewBag.case_manager = "الدعم النفسي -مسؤول تأهيل منزلي";
                    if (rm.Equals("cmPsychologicalSupport3"))
                        ViewBag.case_manager = "الدعم النفسي -مسؤول أنشطة";
                    if (rm.Equals("cmDayCare"))
                        ViewBag.case_manager = "الرعاية نهارية";
                    if (rm.Equals("cmHomeCare"))
                        ViewBag.case_manager = "الرعاية منزلية";
                    if (rm.Equals("cmAwareness"))
                        ViewBag.case_manager = "التوعية";
                    if (rm.Equals("cmSmallProjects"))
                        ViewBag.case_manager = "المشاريع صغيرة";
                    if (rm.Equals("cmSGBV"))
                        ViewBag.case_manager = "SGBV";
                    //if (rm.Equals("cmIOutReachTeam"))
                    //{
                    //    // جلب كل الاحالة لوصول ما ضم مركز معين ما عدا إحالات SGBV 
                    //    var role_id2 = db.AspNetRoles.Where(r => r.Name == "cmSGBV").First().Id;
                    //    ViewBag.role_id = db.AspNetRoles.Where(r => r.Name == "cmIOutReachTeam").First().Id; ;
                    //    //  referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK ==user.idcenter_FK).OrderBy(r => r.submittingdate).Take(100);
                    //    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK != role_id2 && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month).Take(500);
                    //    ViewBag.case_manager = "فريق وصول";
                    //    ViewBag.temporal = db.temporals.Where(t => t.idcenter_FK == user.idcenter_FK).ToList();
                    //}
                    if (rm.Equals("cmInkindAssistance"))
                        ViewBag.case_manager = "المساعدات العينية";

                }

            }
            if (referalpersons == null)
            {
                return HttpNotFound();
            }
            else
            {
                //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
                return View(await referalpersons.ToListAsync());
            }


        }
        [System.Web.Mvc.Authorize(Roles = "superadmin,cmIOutReachTeam")]
        public async Task<ActionResult> IndexOutReach()
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            string userid = User.Identity.GetUserId();
            // هون اعتمدت على يوزر آي دي .. لأنو ما في ربطة نظامية مع اليوزر يلي هوي مستقب الإحالة
            ViewBag.CountNotficationOffline = db.temporals.Where(t => t.servicestate == "Pending" && t.referalstate == "Pending" && t.referalreicver_FK == userid).Count();

            var referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
            var roles = user.AspNetRoles.ToList();
            if (User.IsInRole("cmIOutReachTeam"))
            {
                //return first 100 row order by submit  date
                var role_id = db.AspNetRoles.Where(r => r.Name == "cmSGBV").First().Id;
                ViewBag.role_id = db.AspNetRoles.Where(r => r.Name == "cmIOutReachTeam").First().Id;
                //  referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.AspNetUser.idcenter_FK==user.idcenter_FK).OrderBy(r => r.submittingdate);
                referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK != role_id && r.idcenter_FK == user.idcenter_FK).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                ViewBag.temporal = db.temporals.Where(t => t.idcenter_FK == user.idcenter_FK).ToList();
                ViewBag.case_manager = "فريق وصول";
            }
            //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
            return View(await referalpersons.ToListAsync());
        }

        [System.Web.Mvc.Authorize(Roles = "superadmin,coEducation,coProfessional,coChildProtection,coPsychologicalSupport,coDayCare,coHomeCare,coAwareness,coSmallProjects,coOutReachTeam,coInkindAssistance")]
        // GET: referalpersons
        public async Task<ActionResult> IndexCo()
        {
            //if user in role then retuen row by role
            //by role (case manager) type  we return referal's people
            //----------------------------------------------المنسقين--------------------
            // coEducation       
            //coProfessional
            //coChildProtection
            //coPsychologicalSupport
            //coDayCare
            //coHomeCare
            //coSGBV<<<<<<<<<<<<<<<<<<<<لايوجد اطلاع على الاحالات
            //coSmallProjects
            //coOutReachTeam
            //coInkindAssistance
            //return first 100 row order by submit  date
            //يتم الفلترة بعد تحديد المستخدم ثم الحصول على اسم الرول ثم الحصول  على  مجدول الخدمات على  الخدمات المرتبة بهذه الرول

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var referalpersons = (IQueryable<referalperson>)null;
            var roles = user.AspNetRoles.ToList();
            if (roles.Count > 0)
            {

                //  roles.First().Name
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmEducation").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "تعليمي";
                }
                else if (User.IsInRole("coProfessional"))
                {
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmProfessional").First().idcase;
                    ViewBag.idcase = idcase;
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "مهني";
                }
                else if (User.IsInRole("coChildProtection"))
                {
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmChildProtection").First().idcase;
                    ViewBag.idcase = idcase;
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "حماية الطفل";
                }
                else if (User.IsInRole("coPsychologicalSupport"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport1").First().idcase;
                    var idcase2 = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport2").First().idcase;
                    var idcase3 = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport3").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase || r.service.idcase_FK == idcase2 || r.service.idcase_FK == idcase3).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "الدعم النفسي";
                }

                else if (User.IsInRole("coDayCare"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmDayCare").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "رعاية نهارية";
                }
                else if (User.IsInRole("coHomeCare"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmHomeCare").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "رعاية منزلية";
                }
                else if (User.IsInRole("coAwareness"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmAwareness").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "التوعية ";
                }
                else if (User.IsInRole("coSmallProjects"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmSmallProjects").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "مشاريع صغيرة";
                }

                else if (User.IsInRole("coOutReachTeam"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmSGBV").First().idcase;
                    ViewBag.idcase = db.AspNetRoles.Where(r => r.Name == "cmIOutReachTeam").First().idcase; ;
                    //  referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idrole_FK == role_id && r.idcenter_FK ==user.idcenter_FK).OrderBy(r => r.submittingdate).Take(100);
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK != idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.temporal = db.temporals.ToList();
                    ViewBag.case_manager = "فريق وصول";
                }
                else if (User.IsInRole("coInkindAssistance"))
                {
                    //return first 100 row order by submit  date
                    var idcase = db.AspNetRoles.Where(r => r.Name == "cmInkindAssistance").First().idcase;
                    ViewBag.idcase = idcase;
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    ViewBag.case_manager = "المساعدات العينية";
                }
            }
            if (referalpersons == null)
            {
                return HttpNotFound();
            }
            else
            {
                //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
                return View(await referalpersons.ToListAsync());
            }

        }

        [System.Web.Mvc.Authorize(Roles = "superadmin,coEducation,coProfessional,coChildProtection,coPsychologicalSupport,coDayCare,coHomeCare,coAwareness,coSmallProjects,coOutReachTeam,coInkindAssistance")]
        public async Task<ActionResult> IndexCoByRN(string rm)
        {
            //if user in role then retuen row by role
            //by role (case manager) type  we return referal's people
            //----------------------------------------------المنسقين--------------------
            // coEducation       
            //coProfessional
            //coChildProtection
            //coPsychologicalSupport
            //coDayCare
            //coHomeCare
            //coSGBV<<<<<<<<<<<<<<<<<<<<لايوجد اطلاع على الاحالات
            //coSmallProjects
            //coOutReachTeam
            //coInkindAssistance
            //return first 100 row order by submit  date
            //يتم الفلترة بعد تحديد المستخدم ثم الحصول على اسم الرول ثم الحصول  على  مجدول الخدمات على  الخدمات المرتبة بهذه الرول

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var referalpersons = (IQueryable<referalperson>)null;
            var roles = user.AspNetRoles.ToList();
            if (roles.Count > 0)
            {
                if (User.IsInRole(rm) || rm.Equals("psychologicalSupport"))
                {
                    int? idcase = 0;
                    if (!rm.Equals("psychologicalSupport"))
                    {
                        idcase = db.AspNetRoles.Where(r => r.Name == rm).First().idcase;
                        ViewBag.idcase = idcase;

                        referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                    }
                    if (rm.Equals("cmEducation"))
                        ViewBag.case_manager = "تعليمي";
                    if (rm.Equals("cmProfessional"))
                        ViewBag.case_manager = "مهني";
                    if (rm.Equals("cmChildProtection"))
                        ViewBag.case_manager = "حماية الطفل";
                    if (rm.Equals("psychologicalSupport"))
                    {

                        idcase = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport1").First().idcase;
                        var idcase2 = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport2").First().idcase;
                        var idcase3 = db.AspNetRoles.Where(r => r.Name == "cmPsychologicalSupport3").First().idcase;
                        ViewBag.idcase = idcase;
                        referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK == idcase || r.service.idcase_FK == idcase2 || r.service.idcase_FK == idcase3).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);
                        ViewBag.case_manager = "الدعم النفسي";
                    }

                    if (rm.Equals("cmDayCare"))
                        ViewBag.case_manager = "رعاية نهارية";
                    if (rm.Equals("cmHomeCare"))
                        ViewBag.case_manager = "رعاية منزلية";
                    if (rm.Equals("cmAwareness"))
                        ViewBag.case_manager = "التوعية ";
                    if (rm.Equals("cmSmallProjects"))
                        ViewBag.case_manager = "مشاريع صغيرة";
                    if (rm.Equals("coOutReachTeam"))
                    {
                        //انتبه الخدمة مرتبطة مع الرول(الدور) لمدير الحالة

                        idcase = db.AspNetRoles.Where(r => r.Name == "cmSGBV").First().idcase;
                        ViewBag.idcase = db.AspNetRoles.Where(r => r.Name == "cmIOutReachTeam").First().idcase; ;

                        referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.service.idcase_FK != idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month);

                        ViewBag.temporal = db.temporals.ToList();
                        ViewBag.case_manager = "فريق وصول";

                    }
                    if (rm.Equals("cmInkindAssistance"))
                        ViewBag.case_manager = "المساعدات العينية";
                }

            }
            if (referalpersons == null)
            {
                return HttpNotFound();
            }
            else
            {
                //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
                return View(await referalpersons.ToListAsync());
            }
        }

        // this get new referalperson when added to database .. and when user click on جرس

        public JsonResult GetNotificationsReferal()
        {

            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            string usertosend = db.AspNetUsers.SingleOrDefault(u => u.UserName == User.Identity.Name).Id;

            string validate;
            if (User.IsInRole("cmIOutReachTeam"))
            {
                validate = "outreach volunteer";
                var list = NC.GetTemporal(notificationRegisterTime, usertosend);
                Session["LastUpdated"] = DateTime.Now;
                var result = new { validate = validate, data = list.Select(r => new { idperson_FK = r.idperson, personname = r.fname + " " + r.fathername + " " + r.lname, referaldate = r.submittingdate.Value.ToString("dd-MM-yyyy"), senderuserrole = r.AspNetUser1.AspNetRoles.FirstOrDefault().NameAR, center = r.center.name }) };
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            else
            {
                validate = "else";
                var list = NC.GetReferals(notificationRegisterTime, usertosend);
                Session["LastUpdated"] = DateTime.Now;
                var result = new { validate = validate, data = list.Select(r => new { idreferalperson = r.idreferalperson, idperson_FK = r.idperson_FK, idcase_FK = r.idcase_FK, personname = r.person.fname + " " + r.person.fathername + " " + r.person.lname, serviceType = r.service.name, referaldate = r.submittingdate.Value.ToString("dd-MM-yyyy"), senderuserrole = r.AspNetUser.AspNetRoles.FirstOrDefault().NameAR, center = r.center.name }) };
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public ActionResult FillServices(string caseId, string centerId)
        {
            // List<service> servicedata = new List<service>();
            int idcase = Int32.Parse(caseId);
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<service> services_case = db.services.Where(u => u.centerservices.Any(s => s.idservice_FK == u.idservice && s.enabled && s.center.idcenter == centerId) && u.idcase_FK == idcase).ToList();
            //List<service> services_case = db.services.Where(c => c.idcase_FK == id).OrderByDescending(ser => ser.idservice).ToList();
            List<serviceViewModel> jsonservices = new List<serviceViewModel>();
            for (int i = 0; i < services_case.Count; i++)
            {

                service s = services_case.ElementAt(i);
                serviceViewModel ss = new serviceViewModel();
                ss.idservice = s.idservice;
                ss.name = s.name;


                List<UserViewModel> user_list = new List<UserViewModel>();
                List<AspNetUser> recivers_users = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == s.idrole_FK) && u.center.idcenter == centerId && u.Id != user.Id).ToList();

                foreach (var u in recivers_users)
                {
                    UserViewModel uvm = new UserViewModel();
                    uvm.Id = u.Id;
                    uvm.Email = u.Email;
                    uvm.UserName = u.UserName + "-" + u.center.name;

                    user_list.Add(uvm);
                }
                //في حال لايوجد مستقبلين للخدم وهذا مستحيل 
                if (recivers_users.Count == 0)
                {
                    UserViewModel uvm = new UserViewModel();
                    uvm.Id = "0";
                    uvm.Email = "لايوجد مستقبل";
                    uvm.UserName = "لايوجد مستقبل";
                    user_list.Add(uvm);

                }

                ss.recivers = user_list;


                jsonservices.Add(ss);
            }

            if (services_case.Count == 0)
            {
                serviceViewModel ss = new serviceViewModel();
                ss.idservice = 0;
                ss.name = "لايوجد خدمات مفّعلة";

                UserViewModel uvm = new UserViewModel();
                uvm.Id = "0";
                uvm.Email = "لايوجد مستقبل";
                uvm.UserName = "لايوجد مستقبل";
                List<UserViewModel> user_list = new List<UserViewModel>();
                user_list.Add(uvm);

                ss.recivers = user_list;
                jsonservices.Add(ss);
            }

            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //string result = javaScriptSerializer.Serialize(jsonservices);
            return Json(jsonservices, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FillRecivers(string serviceId, string centerId)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

            int id = Int32.Parse(serviceId);

            service s = db.services.Find(id);
            var currentuser = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<UserViewModel> user_list = new List<UserViewModel>();
            //   List<AspNetUser> recivers_users = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == s.idrole_FK) && u.idcenter_FK == currentuser.idcenter_FK).ToList();
            List<AspNetUser> recivers_users = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == s.idrole_FK) && u.center.idcenter == centerId && u.Id != user.Id).ToList();
            foreach (var u in recivers_users)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.Id = u.Id;
                uvm.Email = u.Email;
                uvm.UserName = u.UserName + "-" + u.center.name;

                user_list.Add(uvm);
            }

            if (recivers_users.Count == 0)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.Id = "0";
                uvm.Email = "لايوجد مستقبل";
                uvm.UserName = "لايوجد مستقبل";
                user_list.Add(uvm);

            }

            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //string result = javaScriptSerializer.Serialize(jsonservices);
            return Json(user_list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchReferalByName(string name, string idrole)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

            List<referalperson> referalpersons = null;
            if (User.IsInRole("cmIOutReachTeam"))
                referalpersons = db.referalpersons.Where(r => (r.person.fname.Contains(name) || r.person.lname.Contains(name) || (r.person.fname + " " + r.person.lname).Contains(name)) && r.idcenter_FK == user.idcenter_FK).ToList();
            else
                referalpersons = db.referalpersons.Where(r => (r.person.fname.Contains(name) || r.person.lname.Contains(name) || (r.person.fname + " " + r.person.lname).Contains(name)) && r.service.idrole_FK == idrole && r.idcenter_FK == user.idcenter_FK).ToList();
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

                //referal type
                rps.type = getReferalType(r.servicestate, r.referalstate);

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
                rps.personEevalution = r.person.evaluation;
                rps.familyEvalution = r.person.family.evaluation;

                rps.referalsender_FK = db.AspNetUsers.Find(r.referalsender_FK).Email;

                RPSearchViewModels.Add(rps);
            }

            return Json(RPSearchViewModels, JsonRequestBehavior.AllowGet);
        }


        public ActionResult searchReferalByDate(string from, string to, string idrole)
        {
            DateTime from_date = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            DateTime to_date = DateTime.ParseExact(to, "yyyy-MM-dd", null);
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<referalperson> referalpersons = null;
            if (User.IsInRole("cmIOutReachTeam"))
                referalpersons = db.referalpersons.Where(r => r.submittingdate > from_date && r.submittingdate < to_date && r.idcenter_FK == user.idcenter_FK).ToList();
            else
                referalpersons = db.referalpersons.Where(r => r.submittingdate > from_date && r.submittingdate < to_date && r.service.idrole_FK == idrole && r.idcenter_FK == user.idcenter_FK).ToList();

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


                //referal type
                rps.type = getReferalType(r.servicestate, r.referalstate);

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

                rps.personEevalution = r.person.evaluation;
                rps.familyEvalution = r.person.family.evaluation;

                rps.referalsender_FK = db.AspNetUsers.Find(r.referalsender_FK).Email;

                RPSearchViewModels.Add(rps);
            }

            return Json(RPSearchViewModels, JsonRequestBehavior.AllowGet);
        }
        public ActionResult searchReferalByNameCO(string name, int idcase)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

            List<referalperson> referalpersons = null;
            if (User.IsInRole("coOutReachTeam"))
                referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => (r.person.fname.Contains(name) || r.person.lname.Contains(name) || (r.person.fname + " " + r.person.lname).Contains(name)) && !r.person.is_secret).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month).ToList();
            else
                referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => (r.person.fname.Contains(name) || r.person.lname.Contains(name) || (r.person.fname + " " + r.person.lname).Contains(name)) && r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month).ToList();

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

                //referal type
                rps.type = getReferalType(r.servicestate, r.referalstate);

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

                rps.personEevalution = r.person.evaluation;
                rps.familyEvalution = r.person.family.evaluation;

                rps.referalsender_FK = db.AspNetUsers.Find(r.referalsender_FK).Email;

                RPSearchViewModels.Add(rps);
            }

            return Json(RPSearchViewModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchReferalByDateCO(string from, string to, int idcase)
        {
            DateTime from_date = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            DateTime to_date = DateTime.ParseExact(to, "yyyy-MM-dd", null);
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<referalperson> referalpersons = null;
            var role_id = db.AspNetRoles.Where(r => r.Name == "cmSGBV").First().Id;
            if (User.IsInRole("coOutReachTeam"))
                referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.submittingdate > from_date && r.submittingdate < to_date && !r.person.is_secret).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month).ToList();
            else
                referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.submittingdate > from_date && r.submittingdate < to_date && r.service.idcase_FK == idcase).OrderByDescending(r => r.submittingdate.Value.Year).OrderByDescending(r => r.submittingdate.Value.Day).OrderByDescending(r => r.submittingdate.Value.Month).ToList();

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


                //referal type
                rps.type = getReferalType(r.servicestate, r.referalstate);

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

                rps.personEevalution = r.person.evaluation;
                rps.familyEvalution = r.person.family.evaluation;

                rps.referalsender_FK = db.AspNetUsers.Find(r.referalsender_FK).Email;

                RPSearchViewModels.Add(rps);
            }

            return Json(RPSearchViewModels, JsonRequestBehavior.AllowGet);
        }

        public string getReferalType(string servicestate, string referalstate)
        {
            string type = "";
            if (servicestate == "Pending" && referalstate == "Pending")
                type = "جديد";
            else if (servicestate == "Pending" && referalstate == "Approved")
                type = "مقبولة";
            else if (servicestate == "Pending" && referalstate == "OutReach")
                type = " مقبولة-وصول";
            else if (servicestate == "Pending" && referalstate == "Rejected")
                type = "مرفوضة";
            else if (servicestate == "Pending" && referalstate == "External")
                type = "خارجي";
            else if (servicestate == "In prgress" && referalstate == "Approved")
                type = "قيد المتابعة";
            else if (servicestate == "In prgress" && referalstate == "OutReach")
                type = "قيد المتابعة-وصول";
            else if (servicestate == "Closed" && referalstate == "Approved")
                type = "مغلقة";
            else if (servicestate == "Closed" && referalstate == "OutReach")
                type = "مغلقة-وصول";

            return type;

        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> editreferalrow(int idreferalperson, int idperson, int idcase, string recieverevalution)
        {
            try
            {
                referalperson rp = db.referalpersons.Find(idreferalperson, idperson, idcase);
                rp.recieverevalution = recieverevalution;

                db.Entry(rp).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return new JsonResult
                {
                    Data = "sucsses"
                ,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = "error" };
            }
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
        public ActionResult personReferal(int? id)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            referalperson rp = new referalperson();

            person per = db.people.Find(id);
            rp.person = per;

            ViewBag.idperson_FK = new SelectList(db.people.Where(p => p.idperson == id), "idperson", "fname", id);

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(u => u.UserName == user.UserName), "Id", "UserName");


            var firstcaseid = db.cases.Where(c => !c.name.Contains("وصول")).First().idcase;
            ViewBag.idcase_FK = new SelectList(db.cases.Where(c => !c.name.Contains("وصول")), "idcase", "name", firstcaseid);
            // List<service> service = db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled).ToList();
            //List<service> service = db.services.Where(s => s.idcase_FK == firstcaseid ).ToList();
            List<service> service = db.services.Where(u => u.centerservices.Any(s => s.idservice_FK == u.idservice && s.enabled && s.center.idcenter == user.center.idcenter) && u.idcase_FK == firstcaseid).ToList();
            service first_service_firstcase;
            if (service.Count != 0)
            {
                //first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled).First();
                //ViewBag.services = new SelectList(db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled), "idservice", "name", first_service_firstcase.name);
                //first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid ).First();
                //ViewBag.services = new SelectList(db.services.Where(s => s.idcase_FK == firstcaseid ), "idservice", "name", first_service_firstcase.name);
                first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid).First();
                ViewBag.services = new SelectList(db.services.Where(u => u.centerservices.Any(s => s.idservice_FK == u.idservice && s.enabled && s.center.idcenter == user.center.idcenter) && u.idcase_FK == firstcaseid), "idservice", "name", first_service_firstcase.idservice);

                List<AspNetUser> recivers = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == first_service_firstcase.idrole_FK) && u.center.idcenter == user.center.idcenter && u.Id != user.Id).ToList();//<<<<<<<<<<<<<<<get all recivers in all centers
                IEnumerable<SelectListItem> items = recivers.Select(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.UserName + "-" + c.center.name

                });
                //  ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == first_service_firstcase.idrole_FK) && u.idcenter_FK == user.idcenter_FK), "Id", "UserName");
                ViewBag.referalReciver_FK = items;

            }
            else
            {
                List<service> ss = new List<Models.service>();
                service s = new service();
                s.idservice = 0;
                s.name = "لايوجد خدمات مفّعلة";
                ss.Add(s);
                List<AspNetUser> uu = new List<Models.AspNetUser>();
                AspNetUser u = new AspNetUser();
                u.Id = "0";
                u.UserName = "لايوجد مستقبل";
                uu.Add(u);
                ViewBag.services = new SelectList(ss, "idservice", "name", "لايوجد خدمات مفّعلة");
                ViewBag.referalReciver_FK = new SelectList(uu, "Id", "UserName", "لايوجد مستقبل");
            }


            ViewBag.idcenter_FK = new SelectList(db.centers.Where(c => c.idcenter == user.idcenter_FK), "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");

            ViewBag.centers = new SelectList(db.centers, "idcenter", "name", user.center.idcenter);

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
        public ActionResult personReferalByCaseManager(int? id, int idcase)
        {
            @case case_man = db.cases.Find(idcase);
            if (case_man.name.Contains("وصول"))
            {
                idcase = db.cases.Where(r => r.idcase != idcase).First().idcase;
            }
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            referalperson rp = new referalperson();

            person per = db.people.Where(p => p.idperson == id).First();
            rp.person = per;

            ViewBag.idperson_FK = new SelectList(db.people.Where(p => p.idperson == id), "idperson", "fname", id);

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(u => u.UserName == user.UserName), "Id", "UserName");

            //  var firstcaseid = db.cases.Where(r => r.idcase != idcase).First().idcase;

            //List<service> service = db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled).ToList();
            //List<service> service = db.services.Where(s => s.idcase_FK == firstcaseid).ToList();
            List<service> service = db.services.Where(u => u.centerservices.Any(s => s.idservice_FK == u.idservice && s.enabled && s.center.idcenter == user.center.idcenter) && u.idcase_FK == idcase).ToList();
            ViewBag.idcase_FK = new SelectList(db.cases.Where(c => !c.name.Contains("وصول")), "idcase", "name", idcase);

            service first_service_firstcase;
            if (service.Count != 0)
            {
                //first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled).First();
                //ViewBag.services = new SelectList(db.services.Where(s => s.idcase_FK == firstcaseid && s.enabled), "idservice", "name", first_service_firstcase.name);
                //first_service_firstcase = db.services.Where(s => s.idcase_FK == firstcaseid ).First();
                //ViewBag.services = new SelectList(db.services.Where(s => s.idcase_FK == firstcaseid ), "idservice", "name", first_service_firstcase.name);
                first_service_firstcase = db.services.Where(s => s.idcase_FK == idcase).First();
                ViewBag.services = new SelectList(db.services.Where(u => u.centerservices.Any(s => s.idservice_FK == u.idservice && s.enabled && s.center.idcenter == user.center.idcenter) && u.idcase_FK == idcase), "idservice", "name", first_service_firstcase.idservice);

                List<AspNetUser> recivers = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == first_service_firstcase.idrole_FK) && u.center.idcenter == user.center.idcenter && u.Id != user.Id).ToList();//<<<<<<<<<<<<<<<get all recivers in all centers
                IEnumerable<SelectListItem> items = recivers.Select(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.UserName + "-" + c.center.name

                });
                //  ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == first_service_firstcase.idrole_FK) && u.idcenter_FK == user.idcenter_FK), "Id", "UserName");
                ViewBag.referalReciver_FK = items;
            }
            else
            {
                List<service> ss = new List<Models.service>();
                service s = new service();
                s.idservice = 0;
                s.name = "لايوجد خدمات مفّعلة";
                ss.Add(s);
                List<AspNetUser> uu = new List<Models.AspNetUser>();
                AspNetUser u = new AspNetUser();
                u.Id = "0";
                u.UserName = "لايوجد مستقبل";
                uu.Add(u);
                ViewBag.services = new SelectList(ss, "idservice", "name", "لايوجد خدمات مفّعلة");
                ViewBag.referalReciver_FK = new SelectList(uu, "Id", "UserName", "لايوجد مستقبل");
            }


            ViewBag.idcenter_FK = new SelectList(db.centers.Where(c => c.idcenter == user.idcenter_FK), "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");
            ViewBag.centers = new SelectList(db.centers, "idcenter", "name", user.center.idcenter);

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

                    rp.idcenter_FK = db.AspNetUsers.Find(rpvm.referalreciever_FK).center.idcenter;
                    rp.referalsender_FK = User.Identity.GetUserId();

                    rp.outreachnote = rpvm.outreachnote;


                    db.referalpersons.Add(rp);
                    await db.SaveChangesAsync();


                    string username = db.AspNetUsers.SingleOrDefault(user => user.Id == rp.referalreicver_FK).UserName;

                    NotificationHub.sendnotify(username, "added");
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
        public async Task<ActionResult> Edit(int? idreferalperson, int idperson, int? idcase)
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

            string state = "xxxxx";
            referalperson rr = referalperson;

            state = getReferalType(rr.servicestate, rr.referalstate);

            ViewBag.state = state;

            return View(referalperson);
        }

        // POST: referalpersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit1")]
        public async Task<ActionResult> edit1([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {

            if (ModelState.IsValid)
            {
                referalperson.referalstate = "Pending";
                referalperson.servicestate = "Pending";
                referalperson.referaldate = DateTime.Now;

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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit2")]
        public async Task<ActionResult> edit2([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "Approved";
                referalperson.servicestate = "Pending";
                referalperson.referaldate = DateTime.Now;
                referalperson.servicestartdate = null;
                referalperson.serviceenddate = null;
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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit3")]
        public async Task<ActionResult> edit3([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "OutReach";
                referalperson.servicestate = "Pending";
                referalperson.referaldate = DateTime.Now;
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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit4")]
        public async Task<ActionResult> edit4([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "Rejected";
                referalperson.servicestate = "Pending";
                referalperson.referaldate = DateTime.Now;
                referalperson.servicestartdate = null;
                referalperson.serviceenddate = null;
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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit5")]
        public async Task<ActionResult> edit5([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "Approved";
                referalperson.servicestate = "In prgress";
                referalperson.referaldate = DateTime.Now;
                referalperson.servicestartdate = DateTime.Now;
                referalperson.serviceenddate = null;
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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit6")]
        public async Task<ActionResult> edit6([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {

            if (ModelState.IsValid)
            {
                referalperson.referalstate = "OutReach";
                referalperson.servicestate = "In prgress";
                referalperson.referaldate = DateTime.Now;

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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit7")]
        public async Task<ActionResult> edit7([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "Approved";
                referalperson.servicestate = "Closed";
                referalperson.referaldate = DateTime.Now;
                referalperson.serviceenddate = DateTime.Now;
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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit8")]
        public async Task<ActionResult> edit8([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "OutReach";
                referalperson.servicestate = "Closed";
                referalperson.referaldate = DateTime.Now;
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

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "edit9")]
        public async Task<ActionResult> edit9([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        {
            if (ModelState.IsValid)
            {
                referalperson.referalstate = "External";
                referalperson.servicestate = "Pending";
                referalperson.referaldate = DateTime.Now;
                referalperson.servicestartdate = null;
                referalperson.serviceenddate = null;
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "idreferalperson,idperson_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalperson referalperson)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(referalperson).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email", referalperson.referalsender_FK);
        //    ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalperson.idcase_FK);
        //    ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", referalperson.idcenter_FK);
        //    ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", referalperson.idperson_FK);
        //    ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", referalperson.idservice_FK);
        //    return View(referalperson);
        //}

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
