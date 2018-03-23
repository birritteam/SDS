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
using System.Collections;

namespace SDS_SanadDistributedSystem.Controllers
{
    [Authorize(Roles = "coEducation,coProfessional,coChildProtection,coPsychologicalSupport,coDayCare,coHomeCare,coAwareness,coSmallProjects,coOutReachTeam,coInkindAssistance")]
    public class centerservicesController : Controller
    {

        private sds_dbEntities db = new sds_dbEntities();
        private bool[] enable = { true, false };
        
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
            List<centerservice> cs = new List<centerservice>();

            if (roles.Count > 0)
            {
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().idcase;
                    
                    centerservices = db.centerservices.Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
//                else
                if (User.IsInRole("coProfessional"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }

                    }
                }
                //else 
                if (User.IsInRole("coChildProtection"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                // else 
                if (User.IsInRole("coPsychologicalSupport"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                //else 
                if (User.IsInRole("coDayCare"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {

                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                //else 
                if (User.IsInRole("coHomeCare"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                //else 
                if (User.IsInRole("coAwareness"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                //else 
                if (User.IsInRole("coSmallProjects"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);

                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                //else 
                if (User.IsInRole("coOutReachTeam"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }
                //else 
                if (User.IsInRole("coInkindAssistance"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().idcase;
                    centerservices = db.centerservices.Include(c => c.service).Where(r => r.service.idcase_FK == case_role_id);
                    if (centerservices.Count() > 0)
                    {
                        foreach (var item in centerservices)
                        {
                            if (!cs.Contains(item))
                                cs.Add(item); //cs.Concat(new centerservice[] { item });
                        }
                    }
                }

            }

            return View(cs);
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
            List<service> s = new List<service>();
            if (roles.Count > 0)
            {
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
               // else
                if (User.IsInRole("coProfessional"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
               // else 
                if (User.IsInRole("coChildProtection"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coPsychologicalSupport"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coDayCare"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coHomeCare"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coAwareness"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coSmallProjects"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coOutReachTeam"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coInkindAssistance"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
            }

            var displayedServices = s;

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
            List<service> s = new List<service>();
            if (roles.Count > 0)
            {
                if (User.IsInRole("coEducation"))
                {  //return first 100 row order by submit  date
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coEducation").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                // else
                if (User.IsInRole("coProfessional"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coProfessional").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                // else 
                if (User.IsInRole("coChildProtection"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coChildProtection").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coPsychologicalSupport"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coPsychologicalSupport").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coDayCare"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coDayCare").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coHomeCare"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coHomeCare").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coAwareness"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coAwareness").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coSmallProjects"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coSmallProjects").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coOutReachTeam"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coOutReachTeam").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
                //else 
                if (User.IsInRole("coInkindAssistance"))
                {
                    var case_role_id = db.AspNetRoles.Where(r => r.Name == "coInkindAssistance").First().idcase;
                    services = db.services.Where(r => r.idcase_FK == case_role_id).ToList();
                    foreach (var item in services)
                    {
                        if (!s.Contains(item))
                            s.Add(item); //cs.Concat(new centerservice[] { item });
                    }
                }
            }

            var displayedServices = s;


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
