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
using System.Web.Mvc.Html;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class peopleController : Controller
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

            person.family = db.families.SingleOrDefault(f => f.idfamily == id);
            //person.idfamily_FK = id;
            person.idperson = id;
            
            ViewBag.family = person.family;
            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email");
            //ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");

            var managelists = db.managelists;

            var works = new SelectList(managelists.Where(ma => ma.flag == "W"), "idmanagelist", "name");
            ViewBag.currentWorkID = works;
            ViewBag.previousWorkID = works;

            ViewBag.idKnowledgeCenter = new SelectList(managelists.Where(ma => ma.flag == "KC"), "idmanagelist", "name");



            List<IQueryable> weaknesses = new List<IQueryable>();

            weaknesses.Add(managelists.Where(ma => ma.flag == "WM"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WD"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WE"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WC"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WWD"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WWF"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WWS"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WP"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WF"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WL"));

            ViewBag.weaknesses = weaknesses;

            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;

            return View(person);
        }

        // POST: people/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // P.S : 
        // removed formnumber from the Bind parameters 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idperson,fname,lname,nationalnumber,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,registrationdate,idfamily_FK,idcenter_FK,note,iduser")] person person, int currentWorkID, int previousWorkID, int idKnowledgeCenter, int[] weaknesses)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                person.registrationdate = now;

                person.iduser = User.Identity.GetUserId();
                person.idcenter_FK = db.AspNetUsers.SingleOrDefault(u => u.Id == person.iduser).idcenter_FK;

                int? maxcenterform = db.people.Where(p => p.idcenter_FK == person.idcenter_FK).Max(p => p.formnumber) + 1;
                if (maxcenterform != null)
                    person.formnumber = maxcenterform;
                else person.formnumber = 1;

                db.people.Add(person);

                personmanage currentWork = new personmanage()
                {
                    idperson_FK = person.idperson,
                    idmanagelist_FK = currentWorkID,
                    eval = "Current",
                    person = person,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == currentWorkID)
                };
                db.personmanages.Add(currentWork);

                personmanage previousWork = new personmanage()
                {
                    idperson_FK = person.idperson,
                    idmanagelist_FK = previousWorkID,
                    eval = "Previous",
                    person = person,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == previousWorkID)
                };
                db.personmanages.Add(previousWork);


                personmanage knowledgecenter = new personmanage()
                {
                    idperson_FK = person.idperson,
                    idmanagelist_FK = idKnowledgeCenter,
                    person = person,
                    eval = "",
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == idKnowledgeCenter)
                };
                db.personmanages.Add(knowledgecenter);

                if (weaknesses != null)
                    foreach (var i in weaknesses)
                    {
                        personmanage weakness = new personmanage()
                        {
                            idperson_FK = person.idperson,
                            idmanagelist_FK = i,
                            person = person,
                            eval = "",
                            managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == i)
                        };
                        db.personmanages.Add(weakness);
                    }

                await db.SaveChangesAsync();
                //return RedirectToAction("Create");

                return new JsonResult
                {
                    Data = new
                    {
                        idperson = person.idperson,
                        fname = person.fname,
                        lname = person.lname,
                        fathername = person.fathername,
                        mothername = person.mothername,
                        age = DateTime.Today.Year - person.birthday.GetValueOrDefault().Year,
                        gender = person.gender,
                        idfamily = person.idfamily_FK
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult { Data = "Failed" };
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

            var managelists = db.managelists;

            var works = managelists.Where(ma => ma.flag == "W");

            int? selectedCurrentWorkId = 0;
            int? selectedPreviousWorkId = 0;

            foreach (var work in works)
            {
                foreach (var personmanage in work.personmanages)
                {
                    if (personmanage.idperson_FK == id && personmanage.eval.Equals("Current"))
                    {
                        selectedCurrentWorkId = personmanage.idmanagelist_FK;
                    }
                    if (personmanage.idperson_FK == id && personmanage.eval.Equals("Previous"))
                    {
                        selectedPreviousWorkId = personmanage.idmanagelist_FK;
                    }
                }
                //selectedCurrentWorkId = work.personmanages.SingleOrDefault(pm => pm.idperson_FK == id && pm.eval.Equals("Current")).idmanagelist_FK;
                //selectedPreviousWorkId = work.personmanages.SingleOrDefault(pm => pm.idperson_FK == id && pm.eval.Equals("Previous")).idmanagelist_FK;
            }


            int selectedKCID = 0;
            var kc = managelists.Where(ma => ma.flag == "KC");

            foreach (var item in kc)
            {
                foreach (var personmanage in item.personmanages)
                {
                    if (personmanage.idperson_FK == id)
                    {
                        selectedKCID = personmanage.idmanagelist_FK;
                    }
                }
            }

            ViewBag.currentWorkID = new SelectList(works, "idmanagelist", "name", selectedCurrentWorkId);
            ViewBag.previousWorkID = new SelectList(works, "idmanagelist", "name", selectedPreviousWorkId);
            ViewBag.idKnowledgeCenter = new SelectList(kc, "idmanagelist", "name", selectedKCID);

            List<int> selectedML = new List<int>();
            List<IQueryable> weaknesses = new List<IQueryable>();

            foreach (var item in managelists)
            {
                foreach (var personmanage in item.personmanages)
                {
                    if (personmanage.idperson_FK == id)
                    {
                        selectedML.Add(personmanage.idmanagelist_FK);
                    }
                }
            }


            //ViewBag.medicalWeakness = managelists.Where(ma => ma.flag == "WM");
            //ViewBag.disabilityWeakness = managelists.Where(ma => ma.flag == "WD");
            //ViewBag.vulnerableElder = managelists.Where(ma => ma.flag == "WE");
            //ViewBag.vulnerableChild = managelists.Where(ma => ma.flag == "WC");
            //ViewBag.vulnerableWoman = managelists.Where(ma => ma.flag == "WWD");
            //ViewBag.singleMother = managelists.Where(ma => ma.flag == "WWF");
            //ViewBag.GBVSurviver = managelists.Where(ma => ma.flag == "WWS");
            //ViewBag.PsychologicalSupport = managelists.Where(ma => ma.flag == "WP");
            //ViewBag.singleFather = managelists.Where(ma => ma.flag == "WF");
            //ViewBag.documentsAid = managelists.Where(ma => ma.flag == "WL");


            weaknesses.Add(managelists.Where(ma => ma.flag == "WM"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WD"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WE"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WC"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WWD"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WWF"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WWS"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WP"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WF"));
            weaknesses.Add(managelists.Where(ma => ma.flag == "WL"));

            ViewBag.selectedML = selectedML;
            ViewBag.weaknesses = weaknesses;

            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;

            return View(person);
        }

        // POST: people/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson,fname,lname,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,registrationdate,idfamily_FK,idcenter_FK,formnumber,note,iduser,nationalnumber")] person person, int currentWorkID, int previousWorkID, int idKnowledgeCenter, int[] weaknesses)
        {
            List<int> weaknessesList = weaknesses.ToList();
            if (ModelState.IsValid)
            {
                List<personmanage> personmanages = db.personmanages.Where(ps => ps.idperson_FK == person.idperson).ToList();
                db.Entry(person).State = EntityState.Modified;

                if (!personmanages.Equals(null))
                    foreach (personmanage pm in db.personmanages.Where(ps => ps.idperson_FK == person.idperson))
                    {
                        if (pm.eval.Equals("Current"))
                        {
                            if (pm.idmanagelist_FK != currentWorkID)
                            {
                                personmanages.Remove(pm);
                                personmanage currentWork = new personmanage()
                                {
                                    idperson_FK = person.idperson,
                                    idmanagelist_FK = currentWorkID,
                                    eval = "Current",
                                    person = person,
                                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == currentWorkID)
                                };
                                personmanages.Add(currentWork);
                            }
                        }
                        else
                        if (pm.eval.Equals("Previous"))
                        {
                            if (pm.idmanagelist_FK != previousWorkID)
                            {
                                personmanages.Remove(pm);
                                personmanage previousWork = new personmanage()
                                {
                                    idperson_FK = person.idperson,
                                    idmanagelist_FK = previousWorkID,
                                    eval = "Previous",
                                    person = person,
                                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == previousWorkID)
                                };
                                personmanages.Add(previousWork);
                            }

                        }
                        else
                        if (pm.managelist.flag.Equals("KC"))
                        {
                            if (pm.managelist.idmanagelist != idKnowledgeCenter)
                            {
                                personmanages.Remove(pm);
                                personmanage knowledgecenter = new personmanage()
                                {
                                    idperson_FK = person.idperson,
                                    idmanagelist_FK = idKnowledgeCenter,
                                    person = person,
                                    eval = "",
                                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == idKnowledgeCenter)
                                };
                                personmanages.Add(knowledgecenter);
                            }
                        }
                        else //weaknesses
                             // if(personmanage.managelist.flag.Equals())
                        {
                            personmanages.Remove(pm);
                        }

                        //await db.SaveChangesAsync();
                    }

                if (weaknesses != null)
                    foreach (var i in weaknesses)
                    {
                        personmanage weakness = new personmanage()
                        {
                            idperson_FK = person.idperson,
                            idmanagelist_FK = i,
                            person = person,
                            eval = "",
                            managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == i)
                        };
                        personmanages.Add(weakness);
                    }

                person.personmanages = personmanages;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", person.iduser);
            //ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", person.idcenter_FK);
            //ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", person.idfamily_FK);
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
