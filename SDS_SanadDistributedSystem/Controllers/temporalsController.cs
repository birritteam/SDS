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
    public class temporalsController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: temporals
        public async Task<ActionResult> Index()
        {
            var temporals = db.temporals.Include(t => t.center);
            return View(await temporals.ToListAsync());
        }

        // GET: temporals/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temporal temporal = await db.temporals.FindAsync(id);
            if (temporal == null)
            {
                return HttpNotFound();
            }
            return View(temporal);
        }

        // GET: temporals/Create
        public ActionResult Create()
        {
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
            return View();
        }

        // POST: temporals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,flag,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note")] temporal temporal)
        {
            if (ModelState.IsValid)
            {
                db.temporals.Add(temporal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", temporal.idcenter_FK);
            return View(temporal);
        }

        // GET: temporals/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temporal temporal = await db.temporals.FindAsync(id);
            if (temporal == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", temporal.idcenter_FK);
            return View(temporal);
        }

        // POST: temporals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,flag,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note")] temporal temporal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temporal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", temporal.idcenter_FK);
            return View(temporal);
        }

        // GET: temporals/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temporal temporal = await db.temporals.FindAsync(id);
            if (temporal == null)
            {
                return HttpNotFound();
            }
            return View(temporal);
        }

        // POST: temporals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            temporal temporal = await db.temporals.FindAsync(id);
            db.temporals.Remove(temporal);
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
