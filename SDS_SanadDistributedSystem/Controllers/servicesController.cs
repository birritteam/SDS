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

namespace SDS_SanadDistributedSystem.Controllers
{
    [Authorize(Roles = "superadmin,admin")]
    public class servicesController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        private bool[] enable = { true, false };

        // GET: services
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Index()
        {
            var services = db.services.Include(s => s.AspNetRole).Include(s => s.@case);
            return View(await services.ToListAsync());
        }

        // GET: services/Details/5
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service service = await db.services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: services/Create
        [Authorize(Roles = "superadmin,admin")]
        public ActionResult Create()
        {
            ViewBag.idrole_FK = new SelectList(db.AspNetRoles, "Id", "NameAR");
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name");
            ViewBag.enableOptions = enable;

            return View();
        }

        // POST: services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]

        public async Task<ActionResult> Create([Bind(Include = "idservice,idcase_FK,name,enabled,description,idrole_FK")] service service)
        {
            if (ModelState.IsValid)
            {
                db.services.Add(service);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idrole_FK = new SelectList(db.AspNetRoles, "Id", "NameAR", service.idrole_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", service.idcase_FK);
            ViewBag.enableOptions = enable;
            return View(service);
        }

        // GET: services/Edit/5
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service service = await db.services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.idrole_FK = new SelectList(db.AspNetRoles, "Id", "Name", service.idrole_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", service.idcase_FK);
            ViewBag.enableOptions = enable;
            return View(service);
        }

        // POST: services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Edit([Bind(Include = "idservice,idcase_FK,name,enabled,description,idrole_FK")] service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idrole_FK = new SelectList(db.AspNetRoles, "Id", "Name", service.idrole_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", service.idcase_FK);
            ViewBag.enableOptions = enable;
            return View(service);
        }

        // GET: services/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    service service = await db.services.FindAsync(id);
        //    if (service == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(service);
        //}

        //// POST: services/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    service service = await db.services.FindAsync(id);
        //    db.services.Remove(service);
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
