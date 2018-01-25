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
    public class referalfamiliesController : BaseController
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: referalfamilies
        public async Task<ActionResult> Index()
        {
            var referalfamilies = db.referalfamilies.Include(r => r.AspNetUser).Include(r => r.@case).Include(r => r.center).Include(r => r.service).Include(r => r.family);
            return View(await referalfamilies.ToListAsync());
        }

        // GET: referalfamilies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            referalfamily referalfamily = await db.referalfamilies.FindAsync(id);
            if (referalfamily == null)
            {
                return HttpNotFound();
            }
            return View(referalfamily);
        }

        // GET: referalfamilies/Create
        public ActionResult Create()
        {
            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name");
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name");
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature");
            return View();
        }

        // POST: referalfamilies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idreferalfamily,idfamily_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalfamily referalfamily)
        {
            if (ModelState.IsValid)
            {
                db.referalfamilies.Add(referalfamily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email", referalfamily.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalfamily.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", referalfamily.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", referalfamily.idservice_FK);
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", referalfamily.idfamily_FK);
            return View(referalfamily);
        }

        // GET: referalfamilies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            referalfamily referalfamily = await db.referalfamilies.FindAsync(id);
            if (referalfamily == null)
            {
                return HttpNotFound();
            }
            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email", referalfamily.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalfamily.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", referalfamily.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", referalfamily.idservice_FK);
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", referalfamily.idfamily_FK);
            return View(referalfamily);
        }

        // POST: referalfamilies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idreferalfamily,idfamily_FK,idcase_FK,idservice_FK,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalsender_FK,senderevalution,recieverevalution,idcenter_FK,outreachnote")] referalfamily referalfamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referalfamily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.referalsender_FK = new SelectList(db.AspNetUsers, "Id", "Email", referalfamily.referalsender_FK);
            ViewBag.idcase_FK = new SelectList(db.cases, "idcase", "name", referalfamily.idcase_FK);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", referalfamily.idcenter_FK);
            ViewBag.idservice_FK = new SelectList(db.services, "idservice", "name", referalfamily.idservice_FK);
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", referalfamily.idfamily_FK);
            return View(referalfamily);
        }

        // GET: referalfamilies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            referalfamily referalfamily = await db.referalfamilies.FindAsync(id);
            if (referalfamily == null)
            {
                return HttpNotFound();
            }
            return View(referalfamily);
        }

        // POST: referalfamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            referalfamily referalfamily = await db.referalfamilies.FindAsync(id);
            db.referalfamilies.Remove(referalfamily);
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
