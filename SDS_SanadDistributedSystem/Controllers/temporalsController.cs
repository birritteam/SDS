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
        [Authorize(Roles = "receptionist")]
        public async Task<ActionResult> Index()
        {
            var temporals = db.temporals.Include(t => t.center);
            return View(await temporals.ToListAsync());
        }

        // GET: temporals/Details/5
        [Authorize(Roles = "receptionist")]
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
        [Authorize(Roles = "receptionist")]
        public ActionResult Create()
        {
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");


            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;


            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "cmIOutReachTeam") && u.idcenter_FK== user.idcenter_FK), "Id", "UserName");

            return View();
        }

        // POST: temporals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "receptionist")]
        public async Task<ActionResult> Create([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note,referalreicver_FK,senderevalution,outreachnote")] temporal temporal)
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

                temporal.submittingdate = DateTime.Now;
                temporal.referalstate = "Pending";
                temporal.servicestate = "Pending";

                db.temporals.Add(temporal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", temporal.idcenter_FK);
            return View(temporal);
        }

        // GET: temporals/Edit/5
        [Authorize(Roles = "receptionist")]
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

        // GET: temporals/Edit/5
        [Authorize(Roles = "cmIOutReachTeam")]
        public async Task<ActionResult> EditReferal(string id)
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
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<string> serviceStates = new List<string>();
            serviceStates.Add("Pending");
            serviceStates.Add("In prgress");
            serviceStates.Add("Closed");

            List<string> referalstates = new List<string>();
            referalstates.Add("Pending");
            referalstates.Add("Approved");
            referalstates.Add("Rejected");
            referalstates.Add("OutReach");
            referalstates.Add("External");
            ViewBag.referalstate = new SelectList(referalstates, temporal.referalstate);
            ViewBag.servicestate = new SelectList(serviceStates, temporal.servicestate);

            ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "cmIOutReachTeam") && u.idcenter_FK == user.idcenter_FK), "Id", "UserName", temporal.referalreicver_FK);

            return View(temporal);
        }


        [Authorize(Roles = "receptionist")]
        // POST: temporals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalreicver_FK,senderevalution,recieverevalution,outreachnote")] temporal temporal)
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

        [Authorize(Roles = "cmIOutReachTeam")]
        // POST: temporals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReferal([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,tempregistrationdate,idcenter_FK,formnumber,note,submittingdate,referalstate,referaldate,servicestate,servicestartdate,serviceenddate,referalreicver_FK,senderevalution,recieverevalution,outreachnote")] temporal temporal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temporal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "referalpersons", "");
            }
          

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            ViewBag.referalReciver_FK = new SelectList(db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "cmIOutReachTeam") && u.idcenter_FK == user.idcenter_FK), "Id", "UserName", temporal.referalreicver_FK);
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
