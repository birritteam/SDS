﻿using Microsoft.AspNet.Identity;
using SDS_SanadDistributedSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class secretPeopleController : BaseController
    {
        private sds_dbEntities db = new sds_dbEntities();
         DbSet<managelist> managelists;
         SelectList works;
         List<IQueryable> weaknesses;

        private string[]
        gender = { "أنثى", "ذكر" },
        nationality = { "عربي - سوري" },
        martial = { "متزوج(ة)", "عازب(ة)", "مطلق(ة)", "منفصل(ة)", "أرمل(ة)", "مخطوب(ة)" },
        education = { "أمي (لا يعرف القراءة والكتابة)", "سنة واحدة (صف أول)", "سنتان (صف ثاني)", "3 سنوات (صف ثالث)", "4 سنوات (صف رابع)", "5 سنوات (صف خامس)",
                "6 سنوات (صف سادس)", "7 سنوات (صف سابع)", "8 سنوات (صف ثامن)","9 سنوات (صف تايع)","10 سنوات (صف عاشر)","11 سنة (صف حادي عشر)","12 سنة (صف ثاني عشر)",
                "شهادة جامعية","دراسات عليا","تدريب مهني أو تقني" },
        relationtype = { "الشخص نفسه", "أب", "أم", "ابن", "ابنة", "أخ", "أخت", "جد", "جدة", "حفيد", "حفيدة", "صلة قرابة أخرى", "لا يوجد صلة قرابة" },
        educationstate = { "آخر تحصيل", "الوضع الحالي" };



        // GET: secretPeople
        [Authorize(Roles = "cmSGBV")]
        public async Task<ActionResult> Index()
        {
            string iduser = User.Identity.GetUserId();
            //idcenter_FK = 
            //db.AspNetUsers.SingleOrDefault(u => u.Id == person.iduser).idcenter_FK;

            var people = db.people.Include(p => p.AspNetUser).Include(p => p.center).Include(p => p.family).Where(p => p.iduser == iduser);
            return View(await people.ToListAsync());
        }


        // GET: secretPeople/Details/5
        [Authorize(Roles = "cmSGBV")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string iduser = User.Identity.GetUserId();
            person person = await db.people.FindAsync(id);
            if (person == null || person.iduser != iduser)
            {
                return HttpNotFound();
            }

            var managelists = db.managelists;

            var works = managelists.Where(ma => ma.flag == "W");

            //int? selectedCurrentWorkId = 0;
            //int? selectedPreviousWorkId = 0;

            string selectedCurrentWork = "";
            string selectedPreviousWork = "";


            foreach (var work in works)
            {
                foreach (var personmanage in work.personmanages)
                {
                    if (personmanage.idperson_FK == id && personmanage.eval.Equals("Current"))
                    {
                        selectedCurrentWork = personmanage.managelist.name;
                    }
                    if (personmanage.idperson_FK == id && personmanage.eval.Equals("Previous"))
                    {
                        selectedPreviousWork = personmanage.managelist.name;
                    }
                }
            }


            //int selectedKCID = 0;
            string selectedKC = "";

            var kc = managelists.Where(ma => ma.flag == "KC");

            foreach (var item in kc)
            {
                foreach (var personmanage in item.personmanages)
                {
                    if (personmanage.idperson_FK == id)
                    {
                        selectedKC = personmanage.managelist.name;
                    }
                }
            }


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

            List<string> selectedWeaknesses = new List<string>();

            foreach (var weaknessGroup in weaknesses)
            {
                foreach (managelist w in weaknessGroup)
                {
                    if (selectedML.Contains(w.idmanagelist))
                    {
                        selectedWeaknesses.Add(w.name);
                    }
                }
            }

            List<string> selectedDocuments = new List<string>();

            foreach (managelist d in managelists.Where(ml => ml.flag == "D"))
            {
                if (selectedML.Contains(d.idmanagelist))
                {
                    selectedDocuments.Add(d.name);
                }
            }

            ViewBag.selectedDocuments = selectedDocuments;
            ViewBag.selectedWeaknesses = selectedWeaknesses;
            ViewBag.selectedCurrentWork = selectedCurrentWork;
            ViewBag.selectedPreviousWork = selectedPreviousWork;
            ViewBag.selectedKC = selectedKC;
            return View(person);
        }




        // GET: secretPeople/Create
        [Authorize(Roles = "cmSGBV")]
        public ActionResult Create()
        {
            managelists = db.managelists;
            works = new SelectList(managelists.Where(ma => ma.flag == "W"), "idmanagelist", "name");
            weaknesses = new List<IQueryable>();

            ViewBag.currentWorkID = works;
            ViewBag.previousWorkID = works;
            ViewBag.idKnowledgeCenter = new SelectList(managelists.Where(ma => ma.flag == "KC"), "idmanagelist", "name");

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

            ViewBag.documentsOptions = managelists.Where(ml => ml.flag == "D");
            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email");
            //ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
            //ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature");

            return View();
        }

        // POST: secretPeople/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cmSGBV")]
        public async Task<ActionResult> Create([Bind(Include = "fname,lname,nationalnumber,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,registrationdate,idfamily_FK,idcenter_FK,formnumber,note,iduser,is_secret,evaluation")] person person, int currentWorkID, int previousWorkID, int idKnowledgeCenter, int[] weaknesses, int[] documents
            //, DateTime? servicestartdate, string senderevalution,string outreachnote
            )
        {
            
            DateTime now = DateTime.Now;
            person.registrationdate = now;

            person.iduser = User.Identity.GetUserId();
            person.idcenter_FK = db.AspNetUsers.SingleOrDefault(u => u.Id == person.iduser).idcenter_FK;

            int? maxPersonId = db.people.Where(f => f.idcenter_FK == person.idcenter_FK).Max(f => (int?)f.idperson);
            if (maxPersonId != null)
                person.idperson = maxPersonId.GetValueOrDefault() + 1;
            else
            {
                person.idperson = db.centers.SingleOrDefault(c => c.idcenter == person.idcenter_FK).min_person_id;
            }


            int? maxcenterform = db.people.Where(p => p.idcenter_FK == person.idcenter_FK).Max(p => p.formnumber) + 1;
            if (maxcenterform != null)
                person.formnumber = maxcenterform;
            else person.formnumber = 1;




         //   person.idperson = person.idcenter_FK.ToString() + person.formnumber.ToString();
            person.is_secret = true;

            if (ModelState.IsValid)
            {
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

                if (documents != null)
                    foreach (int i in documents)
                    {
                        personmanage document = new personmanage()
                        {
                            idperson_FK = person.idperson,
                            idmanagelist_FK = i,
                            person = person,
                            eval = "",
                            managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == i)
                        };
                        db.personmanages.Add(document);
                    }

                //referal data

                referalperson rp = new referalperson();
                //rp.servicestartdate = servicestartdate;
                //rp.senderevalution = person.evaluation.ToString();
              //  rp.outreachnote = person.note;

                rp.idperson_FK = person.idperson;
                rp.idcenter_FK = person.idcenter_FK;
                rp.referaldate = now;
                rp.submittingdate = now;
                rp.servicestartdate = now;

                string idrole = db.AspNetRoles.Where(a => a.Name == "cmSGBV").First().Id;
                int idcase = (int)db.AspNetRoles.Where(a => a.Name == "cmSGBV").First().idcase;
                int idservice = db.services.Where(a => a.idcase_FK == idcase).First().idservice;

                rp.idcase_FK = idcase;
                rp.idservice_FK = idservice;
                rp.referalstate = "Approved";
                rp.servicestate = "In prgress";
                person.referalpersons.Add(rp);


                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", person.iduser);
            //ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", person.idcenter_FK);
            //ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", person.idfamily_FK);

            managelists = db.managelists;
            works = new SelectList(managelists.Where(ma => ma.flag == "W"), "idmanagelist", "name");
            List<IQueryable> weaknesses1 = new List<IQueryable>();

            ViewBag.currentWorkID = works;
            ViewBag.previousWorkID = works;
            ViewBag.idKnowledgeCenter = new SelectList(managelists.Where(ma => ma.flag == "KC"), "idmanagelist", "name");

            weaknesses1.Add(managelists.Where(ma => ma.flag == "WM"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WD"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WE"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WC"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WWD"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WWF"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WWS"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WP"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WF"));
            weaknesses1.Add(managelists.Where(ma => ma.flag == "WL"));

            ViewBag.weaknesses = weaknesses1;

            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;

            ViewBag.documentsOptions = managelists.Where(ml => ml.flag == "D");

            return View(person);
        }

        // GET: secretPeople/Edit/5
        [Authorize(Roles = "cmSGBV")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string iduser = User.Identity.GetUserId();
            person person = await db.people.FindAsync(id);
            if (person == null || person.iduser != iduser)
            {
                return HttpNotFound();
            }

            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", person.iduser);
            //ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", person.idcenter_FK);
            //ViewBag.idfamily_FK = new SelectList(db.families, "idfamily", "familynature", person.idfamily_FK);

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

            ViewBag.documentsOptions = managelists.Where(ml => ml.flag == "D");

            ViewBag.genderOptions = gender;
            ViewBag.nationalityOptions = nationality;
            ViewBag.martialOptions = martial;
            ViewBag.educationstate = educationstate;
            ViewBag.relationtype = relationtype;
            ViewBag.education = education;


            return View(person);
        }
        [Authorize(Roles = "cmSGBV")]
        // POST: secretPeople/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idperson,fname,lname,nationalnumber,fathername,mothername,birthday,birthplace,gender,nationality,martial,relationtype,onoffflag,education,educationstate,phone1,phone2,currentaddress,registrationdate,idfamily_FK,idcenter_FK,formnumber,note,iduser,is_secret")] person person, int currentWorkID, int previousWorkID, int idKnowledgeCenter, int[] weaknesses, int[] documents)
        {
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

                if (documents != null)
                    foreach (var i in documents)
                    {
                        personmanage document = new personmanage()
                        {
                            idperson_FK = person.idperson,
                            idmanagelist_FK = i,
                            person = person,
                            eval = "",
                            managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == i)
                        };
                        personmanages.Add(document);

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

        [Authorize(Roles = "admin")]
        // GET: secretPeople/Delete/5
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

        // POST: secretPeople/Delete/5
        [Authorize(Roles = "admin")]
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
