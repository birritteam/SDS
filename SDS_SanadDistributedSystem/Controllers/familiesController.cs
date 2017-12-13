﻿using System;
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
    public class familiesController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: families
        public async Task<ActionResult> Index(string familyID, string lastName)
        {

            var families = db.families.Include(f => f.AspNetUser);

            if (!String.IsNullOrEmpty(familyID))
            {
                families = families.Where(s => s.idfamily.Contains(familyID));
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                families = families.Where(s => s.lastname.Contains(lastName));
            }

            return View(await families.ToListAsync());
        }

        // GET: families/Details/5
        public async Task<ActionResult> Details(string id)
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

        // GET: families/Create
        public ActionResult Create()
        {
            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.idmangelist = new SelectList(db.managelists.Where(ma => ma.flag == "AT"), "idmanagelist", "name");
            return View();
        }

        // POST: families/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idfamily,familynature,personcount,lastaddress,currentaddress,displacementdate,phone1,phone2,note,iduser,lastname,phone1owner,phone2owner")] family family , int idmangelist)
        {
            if (ModelState.IsValid)
            {
                db.families.Add(family);
                managelist managel = db.managelists.SingleOrDefault(ml => ml.idmanagelist == idmangelist);
                managel.families.Add(family);

                await db.SaveChangesAsync();
                return RedirectToAction("Create","people", new  { id = family.idfamily});
            }

            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            return View(family);
        }

        // GET: families/Edit/5
        public async Task<ActionResult> Edit(string id)
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

            int selectedAddressType= 0;

            foreach (managelist item in db.managelists)
            {
                if (item.families.Contains(family))
                {
                    selectedAddressType = item.idmanagelist;
                }
            }

            ViewBag.idmangelist = new SelectList(db.managelists.Where(ma => ma.flag == "AT"), "idmanagelist", "name", selectedAddressType);


            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            return View(family);
        }

        // POST: families/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idfamily,familynature,personcount,lastaddress,currentaddress,displacementdate,phone1,phone2,note,iduser,lastname,phone1owner,phone2owner")] family family, int idmangelist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                foreach (managelist item in db.managelists)
                {
                    if (item.idmanagelist == idmangelist)
                    {
                        if (!item.families.Contains(family))
                        {
                            item.families.Add(family);
                        }
                    }
                    else
                        item.families.Remove(family);
                }
  
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(family);
        }

        // GET: families/Delete/5
        public async Task<ActionResult> Delete(string id)
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

        // POST: families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
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