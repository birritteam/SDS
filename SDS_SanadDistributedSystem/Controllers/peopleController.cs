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
    public class peopleController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: people
        public async Task<ActionResult> Index(string lastName, string nationalNumber)
        {
            var people = db.people.Include(p => p.AspNetUser).Include(p => p.center).Include(p => p.family);

            if (!String.IsNullOrEmpty(lastName))
            {
                people = people.Where(s => s.lname.Contains(lastName));
            }

            if (!String.IsNullOrEmpty(nationalNumber))
            {
                people = people.Where(s => s.nationalnumber.Contains(nationalNumber));
            }

            return View(await people.ToListAsync());
        }

        // GET: people/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: people/Create
        public ActionResult Create(string id)
        {
            person person = new person();
            person.idperson = id;
            int? maxcenterform = db.people.Where(p => p.idcenter_FK == "SM").Max(p => p.formnumber) + 1;
            person.formnumber = maxcenterform;

            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "idfamily");
            return View(person);
        }

        // POST: people/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idperson,fname,lname,nationalnumber,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,registrationdate,idfamily_FK,idcenter_FK,formnumber,note,iduser")] person person)
        {
            if (ModelState.IsValid)
            {
                
                db.people.Add(person);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", person.iduser);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", person.idcenter_FK);
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "idfamily", person.idfamily_FK);
            return View(person);
        }

        // GET: people/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", person.iduser);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", person.idcenter_FK);
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", person.idfamily_FK);
            return View(person);
        }

        // POST: people/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,registrationdate,idfamily_FK,idcenter_FK,formnumber,note,iduser,nationalnumber")] person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", person.iduser);
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", person.idcenter_FK);
            ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", person.idfamily_FK);
            return View(person);
        }

        // GET: people/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: people/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            person person = await db.people.FindAsync(id);
            db.people.Remove(person);
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
