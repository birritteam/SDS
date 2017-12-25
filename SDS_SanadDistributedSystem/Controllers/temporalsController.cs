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
using Microsoft.AspNet.Identity;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class temporalsController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();

        private string[]
            gender = { "أنثى", "ذكر" },
            nationality = { "عربي - سوري" },
            martial = { "متزوج(ة)", "عازب(ة)", "مطلق(ة)", "منفصل(ة)", "أرمل(ة)", "مخطوب(ة)" },
            education = { "أمي (لا يعرف القراءة والكتابة)", "سنة واحدة (صف أول)", "سنتان (صف ثاني)", "3 سنوات (صف ثالث)", "4 سنوات (صف رابع)", "5 سنوات (صف خامس)",
            "6 سنوات (صف سادس)", "7 سنوات (صف سابع)", "8 سنوات (صف ثامن)","9 سنوات (صف تايع)","10 سنوات (صف عاشر)","11 سنة (صف حادي عشر)","12 سنة (صف ثاني عشر)",
            "شهادة جامعية","دراسات عليا","تدريب مهني أو تقني" },
            relationtype = { "الشخص نفسه", "أب", "أم", "ابن", "ابنة", "أخ", "أخت", "جد", "جدة", "حفيد", "حفيدة", "صلة قرابة أخرى", "لا يوجد صلة قرابة" },
            educationstate = { "آخر تحصيل", "الوضع الحالي" };

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


            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;



            return View();
        }

        // POST: temporals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note")] temporal temporal)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                temporal.tempregistrationdate = now;

                temporal.iduser_FK = User.Identity.GetUserId();
                temporal.idcenter_FK = db.AspNetUsers.SingleOrDefault(u => u.Id == temporal.iduser_FK).idcenter_FK;

                int? maxcenterform = db.temporals.Where(p => p.idcenter_FK == temporal.idcenter_FK).Max(p => p.formnumber) + 1;
                if (maxcenterform != null)
                    temporal.formnumber = maxcenterform;
                else temporal.formnumber = 1;

                temporal.idperson = temporal.idcenter_FK.ToString() + temporal.formnumber.ToString();

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



            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;



            return View(temporal);
        }

        // POST: temporals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note")] temporal temporal)
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
