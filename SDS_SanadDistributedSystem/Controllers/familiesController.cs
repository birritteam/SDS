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
using System.Text.RegularExpressions;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class familiesController : BaseController
    {
        private sds_dbEntities db = new sds_dbEntities();

        private string[] familynatureValues = { "فرد من المجتمع المضيف", "نازح داخلي", "نازح داخلي عائد", "لاجئ عائد إلى سورية", "لاجئ أو طالب لجوء من دولة أخرى" };
        private string[] sectorValues = { "خارج القطاع", "البغطاسية", "الحميدية", "القصور", "الميدان", "حسياء", "حسياء الصناعية", "عشيرة", "فركلوس" };
        private int[] evaluationValues = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        public JsonResult familyBookNumberAlreadyExisted(string family_book_number, int? idfamily)
        {
            bool existed;
            if (idfamily == null)
            {
                existed = db.families.Any(x => x.family_book_number.Equals(family_book_number));
            }
            else
                existed = db.families.Any(x => x.family_book_number.Equals(family_book_number) && x.idfamily != idfamily);
            return Json(!existed, JsonRequestBehavior.AllowGet);
        }
        // GET: families
        [Authorize(Roles = "receptionist,cmIOutReachTeam,caseManager")]
        public async Task<ActionResult> Index(string family_book_number, string lastName, string SN)
        {

            var families = db.families.Include(f => f.AspNetUser);

            if (!String.IsNullOrEmpty(family_book_number))
            {
                families = families.Where(s => s.family_book_number.Contains(family_book_number));
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                families = families.Where(s => s.lastname.Contains(lastName));
            }
            if (!String.IsNullOrEmpty(SN))
            {
                families = families.Where(s => s.serial_number.Contains(SN));
            }
            return View(await families.ToListAsync());
        }

        // GET: families/Details/5
        [Authorize(Roles = "receptionist, caseManager")]
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


            var managelists = db.managelists;

            string selectedCurrentAddress = "";
            string selectedPreviousAddress = "";
            string selectedAddressType = "";
            string selectedKC = "";

            foreach (var m in managelists)
            {
                foreach (var f in m.familymanages)
                {
                    if(f.idfamily_FK == id)
                    {
                        if (m.flag == "A")
                        {

                            if (f.eval.Equals("Current"))
                            {
                                selectedCurrentAddress = m.name;
                            }
                            if (f.eval.Equals("Previous"))
                            {
                                selectedPreviousAddress = m.name;
                            }

                        }
                        else if (m.flag == "AT")
                        {
                            selectedAddressType = m.name;
                        }
                        else if (m.flag == "KC")
                        {
                            selectedKC = m.name;
                        }
                    }
                    
                }
            }



            //foreach (managelist item in db.managelists)
            //{
            //    //if (item.families.Contains(family))
            //    //{
            //    //    selectedAddressType = item.name;
            //    //}
            //}           

            ViewBag.selectedAddressType = selectedAddressType;
            ViewBag.selectedCurrentAddress = selectedCurrentAddress;
            ViewBag.selectedPreviousAddress = selectedPreviousAddress;
            ViewBag.selectedKC = selectedKC;

            return View(family);
        }

        // GET: families/Create
        [Authorize(Roles = "receptionist")]
        //   [AllowAnonymous]
        public ActionResult Create()
        {
            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.addressTypeID = new SelectList(db.managelists.Where(ma => ma.flag == "AT"), "idmanagelist", "name");
            ViewBag.currentAddressID = new SelectList(db.managelists.Where(ma => ma.flag == "A"), "idmanagelist", "name");
            ViewBag.previousAddressID = new SelectList(db.managelists.Where(ma => ma.flag == "A"), "idmanagelist", "name");
            ViewBag.evaluationValues = evaluationValues;
            ViewBag.familynatureValues = familynatureValues;
            ViewBag.sectorValues = sectorValues;
            ViewBag.idKnowledgeCenter = new SelectList(db.managelists.Where(ma => ma.flag == "KC"), "idmanagelist", "name");
            return View();
        }

        // POST: families/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "receptionist")]
        //   [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "familynature,personcount,lastaddress_details,currentaddress_details,displacementdate,phone1,phone2,note,iduser,lastname,phone1owner,phone2owner,evaluation,family_book_number,family_head")] family family, int currentAddressID, int previousAddressID, int addressTypeID, int idKnowledgeCenter)
        {
            if (ModelState.IsValid)
            {
                family.iduser = User.Identity.GetUserId();
                family.idcenter_FK = db.AspNetUsers.SingleOrDefault(u => u.Id == family.iduser).idcenter_FK;


                int? maxFamilyId = db.families.Where(f => f.idcenter_FK == family.idcenter_FK).Max(f => (int?)f.idfamily);
                if (maxFamilyId != null)
                    family.idfamily = maxFamilyId.GetValueOrDefault() + 1;
                else
                {
                    family.idfamily = db.centers.SingleOrDefault(c => c.idcenter == family.idcenter_FK).min_family_id;
                    //  family.idfamily = db.centers;
                }




                int? maxcenterform = db.families.Where(p => p.idcenter_FK == family.idcenter_FK).Max(p => p.formnumber) + 1;
                if (maxcenterform != null)
                    family.formnumber = maxcenterform;
                else family.formnumber = 1;


                family.is_visited = false;


                db.families.Add(family);

                familymanage currentAddress = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = currentAddressID,
                    eval = "Current",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == currentAddressID)
                };
                db.familymanages.Add(currentAddress);

                familymanage previousAddress = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = previousAddressID,
                    eval = "Previous",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == previousAddressID)
                };
                db.familymanages.Add(previousAddress);

                familymanage addressType = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = addressTypeID,
                    eval = "",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == addressTypeID)
                };
                db.familymanages.Add(addressType);

                familymanage knowledgecenter = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = idKnowledgeCenter,
                    family = family,
                    eval = "",
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == idKnowledgeCenter)
                };
                db.familymanages.Add(knowledgecenter);

                // managelist managel = db.managelists.SingleOrDefault(ml => ml.idmanagelist == idmangelist);
                //  managel.families.Add(family);

                await db.SaveChangesAsync();
                return RedirectToAction("Create", "people", new { id = family.idfamily });
            }
            ViewBag.evaluationValues = evaluationValues;
            ViewBag.familynatureValues = familynatureValues;
            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            return View(family);
        }

        // GET: families/Edit/5
        //[AllowAnonymous]
        [Authorize(Roles = "receptionist,cmIOutReachTeam")]
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

            int selectedAddressType = 0;
            int selectedCurrentAddress = 0;
            int selectedPreviousAddress = 0;
            int selectedKCID = 0;

            foreach (familymanage fm in db.familymanages.Where(f => f.idfamily_FK == family.idfamily))
            {
                if (fm.managelist.flag.Equals("AT"))
                {
                    selectedAddressType = fm.idmanagelist_FK;
                }
                else if (fm.managelist.flag.Equals("KC"))
                {
                    selectedKCID = fm.idmanagelist_FK;
                }
                else if (fm.managelist.flag.Equals("A"))
                {
                    if (fm.eval.Equals("Current"))
                    {
                        selectedCurrentAddress = fm.idmanagelist_FK;
                    }
                    else if (fm.eval.Equals("Previous"))
                    {
                        selectedPreviousAddress = fm.idmanagelist_FK;
                    }
                }
            }

            //foreach (managelist item in db.managelists)
            //{
            //    //if (item.families.Contains(family))
            //    //{
            //    //    selectedAddressType = item.idmanagelist;
            //    //}
            //}

            ViewBag.addressTypeID = new SelectList(db.managelists.Where(ma => ma.flag == "AT"), "idmanagelist", "name", selectedAddressType);
            ViewBag.currentAddressID = new SelectList(db.managelists.Where(ma => ma.flag == "A"), "idmanagelist", "name", selectedCurrentAddress);
            ViewBag.previousAddressID = new SelectList(db.managelists.Where(ma => ma.flag == "A"), "idmanagelist", "name", selectedPreviousAddress);
            ViewBag.idKnowledgeCenter = new SelectList(db.managelists.Where(ma => ma.flag == "KC"), "idmanagelist", "name", selectedKCID);

            //ViewBag.idmangelist = new SelectList(db.managelists.Where(ma => ma.flag == "AT"), "idmanagelist", "name", selectedAddressType);
            ViewBag.evaluationValues = evaluationValues;
            ViewBag.familynatureValues = familynatureValues;
            ViewBag.sectorValues = sectorValues;

            //ViewBag.iduser = new SelectList(db.AspNetUsers, "Id", "Email", family.iduser);
            return View(family);
        }

        // POST: families/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[AllowAnonymous]
        [Authorize(Roles = "receptionist,cmIOutReachTeam")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit([Bind(Include = "idfamily,familynature,personcount,lastaddress_details,currentaddress_details,displacementdate,phone1,phone2,note,iduser,lastname,phone1owner,phone2owner,evaluation,formnumber,idcenter_FK,family_book_number,family_head,is_visited ,last_visit_date")] family family, int currentAddressID, int previousAddressID, int addressTypeID, int idKnowledgeCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;


                //List<familymanage> familymanages = db.familymanages.Where(f => f.idfamily_FK == family.idfamily).ToList();

                db.familymanages.RemoveRange(db.familymanages.Where(f => f.idfamily_FK == family.idfamily));

                familymanage currentAddress = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = currentAddressID,
                    eval = "Current",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == currentAddressID)
                };
                db.familymanages.Add(currentAddress);

                familymanage previousAddress = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = previousAddressID,
                    eval = "Previous",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == previousAddressID)
                };
                db.familymanages.Add(previousAddress);

                familymanage addressType = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = addressTypeID,
                    eval = "",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == addressTypeID)
                };
                db.familymanages.Add(addressType);

                familymanage knowledgecenter = new familymanage()
                {
                    idfamily_FK = family.idfamily,
                    idmanagelist_FK = idKnowledgeCenter,
                    eval = "",
                    family = family,
                    managelist = db.managelists.SingleOrDefault(ml => ml.idmanagelist == idKnowledgeCenter)
                };
                db.familymanages.Add(knowledgecenter);



                //foreach (managelist item in db.managelists)
                //{
                //    if (item.idmanagelist == idmangelist)
                //    {
                //        if (!item.families.Contains(family))
                //        {
                //            item.families.Add(family);
                //        }
                //    }
                //    else
                //        item.families.Remove(family);
                //}
                family.people = db.people.Where(p => p.idfamily_FK == family.idfamily).ToList();
                //string digits = "";
                Regex regex = new Regex("[0-9]{1,}");

                foreach (person p in family.people)
                {
                    //digits = new String(p.family_order_id.TakeWhile(Char.IsDigit).ToArray());
                    p.family_order_id = regex.Replace(p.family_order_id, family.family_book_number,1);

                }



                await db.SaveChangesAsync();

                return RedirectToAction("Details", "families",new { id=family.idfamily });
                //if (User.IsInRole("receptionist"))
                //{
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    return RedirectToAction("IndexOutReach", "referalpersons", null);
                //}
            }
            ViewBag.evaluationValues = evaluationValues;
            ViewBag.familynatureValues = familynatureValues;
            ViewBag.idmangelist = new SelectList(db.managelists.Where(ma => ma.flag == "AT"), "idmanagelist", "name", null);
            ViewBag.sectorValues = sectorValues;



            return View(family);
        }

        // GET: families/Delete/5
        [Authorize(Roles = "admin")]
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

        // POST: families/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
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
