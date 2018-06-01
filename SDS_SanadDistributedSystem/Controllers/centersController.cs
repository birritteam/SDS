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

    public class centersController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        [Authorize(Roles = "superadmin,admin")]
        // GET: centers
        public async Task<ActionResult> Index()
        {
            var centers = db.centers.Include(c => c.partner);
            return View(await centers.ToListAsync());
        }
        [Authorize(Roles = "superadmin,admin")]
        // GET: centers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            center center = await db.centers.FindAsync(id);
            if (center == null)
            {
                return HttpNotFound();
            }
            return View(center);
        }

        public JsonResult IsAlreadyUsedFlag(string flag)
        {

            return Json(IsFlagAvailable(flag));

        }
        public bool IsFlagAvailable(string flag)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var regFlag = (from u in db.centers
                           where u.flag == flag
                           select new { flag }).FirstOrDefault();

            bool status;
            if (regFlag != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        [Authorize(Roles = "superadmin,admin")]
        // GET: centers/Create
        public ActionResult Create()
        {
            ViewBag.idpartner_FK = new SelectList(db.partners, "idpartner", "name");
            return View();
        }

        // POST: centers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Create([Bind(Include = "idcenter,name,location,idpartner_FK,flag,min_family_id,max_family_id,min_person_id,max_person_id")] center center)
        {
            if (ModelState.IsValid)
            {
                partner p = db.partners.SingleOrDefault(pa => pa.idpartner == center.idpartner_FK);
                center.idcenter = p.idpartner + center.flag;
                db.centers.Add(center);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idpartner_FK = new SelectList(db.partners, "idpartner", "name", center.idpartner_FK);
            return View(center);
        }
        [Authorize(Roles = "superadmin,admin")]
        // GET: centers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            center center = await db.centers.FindAsync(id);
            if (center == null)
            {
                return HttpNotFound();
            }
            ViewBag.idpartner_FK = new SelectList(db.partners, "idpartner", "name", center.idpartner_FK);
            return View(center);
        }

        // POST: centers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Edit([Bind(Include = "idcenter,name,location,idpartner_FK,flag,min_family_id,max_family_id,min_person_id,max_person_id")] center center)
        {
            ModelState.Remove("flag");
            if (ModelState.IsValid)
            {
                db.Entry(center).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idpartner_FK = new SelectList(db.partners, "idpartner", "name", center.idpartner_FK);
            return View(center);
        }

        // GET: centers/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    center center = await db.centers.FindAsync(id);
        //    if (center == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(center);
        //}

        //// POST: centers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    center center = await db.centers.FindAsync(id);
        //    db.centers.Remove(center);
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
