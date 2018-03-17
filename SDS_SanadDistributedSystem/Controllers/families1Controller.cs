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
    public class families1Controller : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: families1
        public async Task<ActionResult> Index()
        {
            var families = db.families.Include(f => f.AspNetUser).Include(f => f.center);
            return View(await families.ToListAsync());
        }

        // GET: families1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            family family = await db.families.FindAsync(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // GET: families1/Create
        public ActionResult Create()
        {
            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
            return View();
        }

        // POST: families1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idfamily,familynature,lastname,personcount,lastaddress_details,currentaddress_details,displacementdate,phone1,phone2,note,iduser,phone1owner,phone2owner,evaluation,formnumber,idcenter_FK,serial_number,family_book_number,family_head,sector,is_visited,last_visit_date")] family family)
        {
            if (ModelState.IsValid)
            {
                db.families.Add(family);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", family.idcenter_FK);
            return View(family);
        }

        // GET: families1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            family family = await db.families.FindAsync(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", family.idcenter_FK);
            return View(family);
        }

        // POST: families1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idfamily,familynature,lastname,personcount,lastaddress_details,currentaddress_details,displacementdate,phone1,phone2,note,iduser,phone1owner,phone2owner,evaluation,formnumber,idcenter_FK,serial_number,family_book_number,family_head,sector,is_visited,last_visit_date")] family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", family.idcenter_FK);
            return View(family);
        }

        // GET: families1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            family family = await db.families.FindAsync(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // POST: families1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            family family = await db.families.FindAsync(id);
            db.families.Remove(family);
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
