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
using Microsoft.AspNet.Identity;

namespace SDS_SanadDistributedSystem.Controllers
{
    [Authorize(Roles = "coEducation,coProfessional,coChildProtection,coPsychologicalSupport,coDayCare,coHomeCare,coAwareness,coSmallProjects,coOutReachTeam,coInkindAssistance")]
    public class centerservicesController : Controller
    {

        private sds_dbEntities db = new sds_dbEntities();
        private bool[] enable = { true, false };

        //public ActionResult fillServices(string centerid)
        //{
        //    var centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).ToList();
        //    var displayCenters = db.centers.ToList();
        //    var user = db.AspNetUsers.Find(User.Identity.GetUserId());
        //    var services = db.services.ToList();
        //    var displayedServices = services;
        //    var roles = user.AspNetRoles.ToList();
        //    var role_id = "";

        //    if (roles.Count > 0)
        //    {
        //        if (User.IsInRole("coEducation"))
        //        {  //return first 100 row order by submit  date
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else
        //        if (User.IsInRole("coProfessional"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coChildProtection"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coPsychologicalSupport"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coDayCare"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coHomeCare"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coAwareness"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coSmallProjects"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coOutReachTeam"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        else if (User.IsInRole("coInkindAssistance"))
        //        {
        //            role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().Id;
        //            services = db.services.Where(r => r.idrole_FK == role_id).ToList();
        //        }
        //        displayedServices = services;
        //        ////checking whether the services is enabled or not, if enabled then it will not be shown in the dropdownlist
        //        foreach (var item in centerservices)
        //        {
        //            if (services.Contains(item.service) && item.enabled == true && item.idcenter_FK == centerid)
        //            {
        //                displayedServices.Remove(item.service);
        //            }
        //        }

        //    }
        //    return Json(displayedServices, JsonRequestBehavior.AllowGet);
        //}

        public async Task<ActionResult> enableService([Bind(Include = "idcenter_FK,idservice_FK,enabled,location,start_date,end_date,Id")] centerservice centerservice, bool enabled)
        {
            if (ModelState.IsValid)
            {
                if (enabled == false)
                    centerservice.end_date = DateTime.Today;
                db.Entry(centerservice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", centerservice.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", centerservice.idservice_FK);
            ViewBag.enableOptions = enable;

            return await Index();
        }
        // GET: centerservices
        public async Task<ActionResult> Index()
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var centerservices = db.centerservices.Include(c => c.center).Include(c => c.service);
            var roles = user.AspNetRoles.ToList();
            var role_id = "";
            if (roles.Count > 0)
            {
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().Id;
                    centerservices = db.centerservices.Where(r => r.service.idrole_FK == role_id);
                    //              centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else
                if (User.IsInRole("coProfessional"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                    //                centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coChildProtection"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                    //                  centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coPsychologicalSupport"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                    //                    centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coDayCare"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coHomeCare"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coAwareness"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coSmallProjects"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coOutReachTeam"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                else if (User.IsInRole("coInkindAssistance"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().Id;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
                }
                //      centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idrole_FK == role_id);
            }
            return View(await centerservices.ToListAsync());
        }

        // GET: centerservices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            centerservice centerservice = await db.centerservices.FindAsync(id);
            if (centerservice == null)
            {
                return HttpNotFound();
            }
            return View(centerservice);
        }

        // GET: centerservices/Create
        public ActionResult Create()
        {
            var centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).ToList();
            //db.centerservices.ToList();
            var centers = db.centers.ToList();
            var displayCenters = db.centers.ToList();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var services = db.services.ToList();
            var roles = user.AspNetRoles.ToList();
            var role_id = "";

            //displaying the centers that doesn't have a current enabled service
            //foreach (var centerItem in centers)
            //{
            //    foreach (var item in centerservices)
            //    {
            //        if (item.idcenter_FK.Equals(centerItem.idcenter) && item.enabled == true)
            //        {
            //            displayCenters.Remove(centerItem);
            //        }
            //    }
            //}
            if (roles.Count > 0)
            {
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else
                if (User.IsInRole("coProfessional"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coChildProtection"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coPsychologicalSupport"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coDayCare"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coHomeCare"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coAwareness"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coSmallProjects"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coOutReachTeam"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coInkindAssistance"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }

            }

            var displayedServices = services;
            ////checking whether the services is enabled or not, if enabled then it will not be shown in the dropdownlist
            //foreach (var item in centerservices)
            //{
            //    if (services.Contains(item.service) && item.enabled == true)
            //    {
            //        displayedServices.Remove(item.service);
            //    }
            //}


            ViewBag.idcenter_FK = new SelectList(displayCenters, "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(displayedServices, "idservice", "name");
            ViewBag.enableOptions = enable;
            return View();
        }

        // POST: centerservices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idcenter_FK,idservice_FK,enabled,location,start_date,end_date,Id")] centerservice centerservice, string[] idcenter_FK)
        {
            if (ModelState.IsValid)
            {
                foreach (string centerid in idcenter_FK)
                {
                    centerservice.idcenter_FK = centerid;
                    db.centerservices.Add(centerservice);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", centerservice.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", centerservice.idservice_FK);
            ViewBag.enableOptions = enable;

            return View(centerservice);
        }

        // GET: centerservices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            centerservice centerservice = await db.centerservices.FindAsync(id);
            if (centerservice == null)
            {
                return HttpNotFound();
            }
            var centerservices = db.centerservices.Include(c => c.center).Include(c => c.service).ToList();
            //db.centerservices.ToList();
            var centers = db.centers.ToList();
            var displayCenters = db.centers.ToList();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var services = db.services.ToList();
            var roles = user.AspNetRoles.ToList();
            var role_id = "";

            //displaying the centers that doesn't have a current enabled service
            //foreach (var centerItem in centers)
            //{
            //    foreach (var item in centerservices)
            //    {
            //        if (item.idcenter_FK.Equals(centerItem.idcenter) && item.enabled == true)
            //        {
            //            displayCenters.Remove(centerItem);
            //        }
            //    }
            //}
            if (roles.Count > 0)
            {
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else
                if (User.IsInRole("coProfessional"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coChildProtection"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coPsychologicalSupport"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coDayCare"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coHomeCare"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coAwareness"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coSmallProjects"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coOutReachTeam"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }
                else if (User.IsInRole("coInkindAssistance"))
                {
                    role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().Id;
                    services = db.services.Where(r => r.idrole_FK == role_id).ToList();
                }

            }

            var displayedServices = services;


            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", centerservice.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(displayedServices, "idservice", "name", centerservice.idservice_FK);
            ViewBag.enableOptions = enable;

            return View(centerservice);
        }

        // POST: centerservices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idcenter_FK,idservice_FK,enabled,location,start_date,end_date,Id")] centerservice centerservice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centerservice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", centerservice.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", centerservice.idservice_FK);
            ViewBag.enableOptions = enable;

            return View(centerservice);
        }

        // GET: centerservices/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    centerservice centerservice = await db.centerservices.FindAsync(id);
        //    if (centerservice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(centerservice);
        //}

        //// POST: centerservices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    centerservice centerservice = await db.centerservices.FindAsync(id);
        //    db.centerservices.Remove(centerservice);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
