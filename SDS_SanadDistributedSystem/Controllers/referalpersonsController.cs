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

namespace SDS_SanadDistributedSystem.Controllers
{
    public class referalpersonsController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        private static List<referalperson> referalpersonlist = new List<referalperson>();
        private static List<referalPersonViewModel> referalpersonlistviewmodel = new List<referalPersonViewModel>();
        // GET: referalpersons
        public async Task<ActionResult> Index()
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

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
            var referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).OrderBy(r => r.submittingdate).Take(100);
            var roles = user.AspNetRoles.ToList();
            if (roles.Count > 0) {
                if (roles.First().Name== "cmEducation")
                {  //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 1).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmProfessional")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 2).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmChildProtection")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 3).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmPsychologicalSupport1")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 4).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmPsychologicalSupport2")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 5).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmPsychologicalSupport3")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 6).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmDayCare")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 7).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmHomeCare")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 8).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmSGBV")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 9).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmSmallProjects")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 10).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmIOutReachTeam")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 11).OrderBy(r => r.submittingdate).Take(100);
                }
                else if (roles.First().Name == "cmInkindAssistance")
                {
                    //return first 100 row order by submit  date
                    referalpersons = db.referalpersons.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.person).Include(r => r.service).Where(r => r.@case.idcase == 12).OrderBy(r => r.submittingdate).Take(100);
                }
            }
            //return View(await referalpersons.Where(x=>x.servicestate == "Pending" && x.referalstate=="Pending").ToListAsync());
            return View(await referalpersons.ToListAsync());
        }

        public ActionResult FillServices(string selectedId)
        {
            // List<service> servicedata = new List<service>();
            int id = Int32.Parse(selectedId);

            List<service> servicedatas = db.services.Where(c => c.idcase_FK == id).ToList();
            List<serviceViewModel> jsonservices = new List<serviceViewModel>();
            foreach (service s in servicedatas)
            {
                serviceViewModel ss = new serviceViewModel();
                ss.idservice = s.idservice;
                ss.name = s.name;
                jsonservices.Add(ss);
            }

            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //string result = javaScriptSerializer.Serialize(jsonservices);
            return Json(jsonservices, JsonRequestBehavior.AllowGet);
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


        //add new referal by Receptionist
        public ActionResult personReferal(string id)
        {
            //List<string> serviceStates = new List<string>();
            //serviceStates.Add("Pending");
            //serviceStates.Add("In prgress");
            //serviceStates.Add("Closed");

            //List<string> referalstates = new List<string>();
            //referalstates.Add("Pending");
            //referalstates.Add("Approved");
            //referalstates.Add("Rejected");
            //referalstates.Add("OutReach");
            //referalstates.Add("External");

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

            ViewBag.idperson_FK = new SelectList(db.people.Where(p => p.idperson == id), "idperson", "fname");

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(u => u.UserName == user.UserName), "Id", "Email");

            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name");
            ViewBag.idcenter_FK = new SelectList(db.centers.Where(c => c.idcenter == user.idcenter_FK), "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");

            ViewBag.cmEducation = new SelectList(db.services.Where(s => s.idcase_FK == 1), "idservice", "name");
            ViewBag.cmProfessional = new SelectList(db.services.Where(s => s.idcase_FK == 2), "idservice", "name");
            ViewBag.cmChildProtection = new SelectList(db.services.Where(s => s.idcase_FK == 3), "idservice", "name");

            ViewBag.cmPsychologicalSupport1 = new SelectList(db.services.Where(s => s.idcase_FK == 4), "idservice", "name");
            ViewBag.cmPsychologicalSupport2 = new SelectList(db.services.Where(s => s.idcase_FK == 5), "idservice", "name");
            ViewBag.cmPsychologicalSupport3 = new SelectList(db.services.Where(s => s.idcase_FK == 6), "idservice", "name");

            ViewBag.cmDayCare = new SelectList(db.services.Where(s => s.idcase_FK == 7), "idservice", "name");
            ViewBag.cmHomeCare = new SelectList(db.services.Where(s => s.idcase_FK == 8), "idservice", "name");

            ViewBag.cmSGBV = new SelectList(db.services.Where(s => s.idcase_FK == 9), "idservice", "name");
            ViewBag.cmSmallProjects = new SelectList(db.services.Where(s => s.idcase_FK == 10), "idservice", "name");
            ViewBag.cmIOutReachTeam = new SelectList(db.services.Where(s => s.idcase_FK == 11), "idservice", "name");

            ViewBag.cmInkindAssistance = new SelectList(db.services.Where(s => s.idcase_FK == 12), "idservice", "name");

            //ViewBag.referalstate = new SelectList(referalstates, "Pending");
            //ViewBag.servicestate = new SelectList(serviceStates, "Pending");

            return View();
        }
        //add new referal by CaseManager
        public ActionResult personReferalByCaseManager(string id, int idcase)
        {
            //List<string> serviceStates = new List<string>();
            //serviceStates.Add("Pending");
            //serviceStates.Add("In prgress");
            //serviceStates.Add("Closed");

            //List<string> referalstates = new List<string>();
            //referalstates.Add("Pending");
            //referalstates.Add("Approved");
            //referalstates.Add("Rejected");
            //referalstates.Add("OutReach");
            //referalstates.Add("External");

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());

            ViewBag.idperson_FK = new SelectList(db.people.Where(p => p.idperson == id), "idperson", "fname");

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(u => u.UserName == user.UserName), "Id", "Email");

            ViewBag.idcase_FK = new SelectList(db.cases.Where(r => r.idcase != idcase), "idcase", "name");
            ViewBag.idcenter_FK = new SelectList(db.centers.Where(c => c.idcenter == user.idcenter_FK), "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");


            ViewBag.cmEducation = new SelectList(db.services.Where(s => s.idcase_FK == 1), "idservice", "name");
            ViewBag.cmProfessional = new SelectList(db.services.Where(s => s.idcase_FK == 2), "idservice", "name");
            ViewBag.cmChildProtection = new SelectList(db.services.Where(s => s.idcase_FK == 3), "idservice", "name");

            ViewBag.cmPsychologicalSupport1 = new SelectList(db.services.Where(s => s.idcase_FK == 4), "idservice", "name");
            ViewBag.cmPsychologicalSupport2 = new SelectList(db.services.Where(s => s.idcase_FK == 5), "idservice", "name");
            ViewBag.cmPsychologicalSupport3 = new SelectList(db.services.Where(s => s.idcase_FK == 6), "idservice", "name");

            ViewBag.cmDayCare = new SelectList(db.services.Where(s => s.idcase_FK == 7), "idservice", "name");
            ViewBag.cmHomeCare = new SelectList(db.services.Where(s => s.idcase_FK == 8), "idservice", "name");

            ViewBag.cmSGBV = new SelectList(db.services.Where(s => s.idcase_FK == 9), "idservice", "name");
            ViewBag.cmSmallProjects = new SelectList(db.services.Where(s => s.idcase_FK == 10), "idservice", "name");
            ViewBag.cmIOutReachTeam = new SelectList(db.services.Where(s => s.idcase_FK == 11), "idservice", "name");

            ViewBag.cmInkindAssistance = new SelectList(db.services.Where(s => s.idcase_FK == 12), "idservice", "name");

            //ViewBag.referalstate = new SelectList(referalstates, "Pending");
            //ViewBag.servicestate = new SelectList(serviceStates, "Pending");

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> sendReferals(List<referalPersonViewModel> referals)
        {
            try
            {
                foreach (referalPersonViewModel rpvm in referals)
                {

                    referalperson rp = new referalperson();
                    rp.idperson_FK = rpvm.idperson_FK;

                    rp.idcase_FK = Int32.Parse(rpvm.idcase_FK);

                    rp.idservice_FK = Int32.Parse(rpvm.idservice_FK);

                    rp.submittingdate = DateTime.Parse(rpvm.submittingdate);

                    //rp.referalstate = rpvm.referalstate;

                    //rp.servicestate = rpvm.servicestate;

                    rp.referalstate = "Pending";

                    rp.servicestate = "Pending";

                    rp.referalsender_FK = rpvm.referalsender_FK;

                    rp.senderevalution = rpvm.senderevalution;

                    rp.idcenter_FK = rpvm.idcenter_FK;

                    rp.outreachnote = rpvm.outreachnote;

                    db.referalpersons.Add(rp);
                    await db.SaveChangesAsync();
                }
                return new JsonResult { Data = "Success send referals" };
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

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers.Where(r=>r.Id== referalperson.referalsender_FK), "Id", "Email", referalperson.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalperson.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers.Where(r=>r.idcenter== referalperson.idcenter_FK), "idcenter", "name", referalperson.idcenter_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", referalperson.idperson_FK);
            ViewBag.idservice_FK = new SelectList(db.services.Where(s => s.idcase_FK == idcase), "idservice", "name", referalperson.idservice_FK);

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
