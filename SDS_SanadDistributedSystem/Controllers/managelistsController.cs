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
    [Authorize(Roles = "admin")]
    public class managelistsController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        public JsonResult flagAlreadyExisted(string flag)
        {
            bool existed = db.managelists.Any(x => x.flag.Equals(flag));
            return Json(!existed, JsonRequestBehavior.AllowGet);
        }


        // GET: managelists
        public async Task<ActionResult> Index()
        {
            return View(await db.managelists.ToListAsync());
        }

        // GET: managelists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            managelist managelist = await db.managelists.FindAsync(id);
            if (managelist == null)
            {
                return HttpNotFound();
            }
            return View(managelist);
        }

        // GET: managelists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: managelists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idmanagelist,name,flag")] managelist managelist)
        {
            if (ModelState.IsValid)
            {
                db.managelists.Add(managelist);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(managelist);
        }

        // GET: managelists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            managelist managelist = await db.managelists.FindAsync(id);
            if (managelist == null)
            {
                return HttpNotFound();
            }
            return View(managelist);
        }

        // POST: managelists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idmanagelist,name,flag")] managelist managelist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managelist).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(managelist);
        }

        // GET: managelists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            managelist managelist = await db.managelists.FindAsync(id);
            if (managelist == null)
            {
                return HttpNotFound();
            }
            return View(managelist);
        }

        // POST: managelists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            managelist managelist = await db.managelists.FindAsync(id);
            db.managelists.Remove(managelist);
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
