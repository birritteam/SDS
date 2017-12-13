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
    public class servicesController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: services
        public async Task<ActionResult> Index()
        {
            var services = db.services.Include(s => s.@case);
            return View(await services.ToListAsync());
        }

        // GET: services/Details/5
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
        public ActionResult Create()
        {
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name");
            return View();
        }

        // POST: services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idservice,idcase_FK,name,enabled")] service service)
        {
            if (ModelState.IsValid)
            {
                db.services.Add(service);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", service.idcase_FK);
            return View(service);
        }

        // GET: services/Edit/5
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
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", service.idcase_FK);
            return View(service);
        }

        // POST: services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idservice,idcase_FK,name,enabled")] service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", service.idcase_FK);
            return View(service);
        }

        // GET: services/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            service service = await db.services.FindAsync(id);
            db.services.Remove(service);
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
