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
    public class personmanagesController : BaseController
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: personmanages
        public async Task<ActionResult> Index()
        {
            var personmanages = db.personmanages.Include(p => p.managelist).Include(p => p.person);
            return View(await personmanages.ToListAsync());
        }

        // GET: personmanages/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personmanage personmanage = await db.personmanages.FindAsync(id);
            if (personmanage == null)
            {
                return HttpNotFound();
            }
            return View(personmanage);
        }

        // GET: personmanages/Create
        public ActionResult Create()
        {
            ViewBag.idmanagelist_FK = new SelectList(db.managelists, "idmanagelist", "name");
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname");
            return View();
        }

        // POST: personmanages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idperson_FK,idmanagelist_FK,eval")] personmanage personmanage)
        {
            if (ModelState.IsValid)
            {
                db.personmanages.Add(personmanage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idmanagelist_FK = new SelectList(db.managelists, "idmanagelist", "name", personmanage.idmanagelist_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", personmanage.idperson_FK);
            return View(personmanage);
        }

        // GET: personmanages/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personmanage personmanage = await db.personmanages.FindAsync(id);
            if (personmanage == null)
            {
                return HttpNotFound();
            }
            ViewBag.idmanagelist_FK = new SelectList(db.managelists, "idmanagelist", "name", personmanage.idmanagelist_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", personmanage.idperson_FK);
            return View(personmanage);
        }

        // POST: personmanages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson_FK,idmanagelist_FK,eval")] personmanage personmanage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personmanage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idmanagelist_FK = new SelectList(db.managelists, "idmanagelist", "name", personmanage.idmanagelist_FK);
            ViewBag.idperson_FK = new SelectList(db.people, "idperson", "fname", personmanage.idperson_FK);
            return View(personmanage);
        }

        // GET: personmanages/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personmanage personmanage = await db.personmanages.FindAsync(id);
            if (personmanage == null)
            {
                return HttpNotFound();
            }
            return View(personmanage);
        }

        // POST: personmanages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            personmanage personmanage = await db.personmanages.FindAsync(id);
            db.personmanages.Remove(personmanage);
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
