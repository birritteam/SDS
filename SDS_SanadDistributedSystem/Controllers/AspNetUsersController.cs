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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace SDS_SanadDistributedSystem.Controllers
{
    [Authorize(Roles = "superadmin,admin")]
    public class AspNetUsersController : BaseController
    {
        private sds_dbEntities db = new sds_dbEntities();
        private bool[] enable = { true, false };

        [Authorize(Roles = "superadmin,admin")]
        // GET: AspNetUsers
        public async Task<ActionResult> Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.center);
            return View(await aspNetUsers.ToListAsync());
        }
        [Authorize(Roles = "superadmin,admin")]
        // GET: AspNetUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }


        public JsonResult IsAlreadySignedUserName(string UserName, string Id)
        {

            return Json(IsUserAvailable(UserName, Id));

        }
        public bool IsUserAvailable(string UserName, string Id)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var RegUserName = (from u in db.AspNetUsers
                               where u.UserName.ToUpper() == UserName.ToUpper() && u.Id != Id
                               select new { UserName }).FirstOrDefault();

            bool status;
            if (RegUserName != null)
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

        public JsonResult IsAlreadySignedEmail(string Email, string Id)
        {

            return Json(IsEmailAvailable(Email, Id));

        }
        public bool IsEmailAvailable(string Email, string Id)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var RegEmail = (from u in db.AspNetUsers
                            where u.Email.ToUpper() == Email.ToUpper() && u.Id != Id
                            select new { Email }).FirstOrDefault();

            bool status;
            if (RegEmail != null)
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

        public JsonResult IsAlreadySignedPhone(string phone, string Id)
        {

            return Json(IsEmailAvailable(phone, Id));

        }
        public bool IsPhoneAvailable(string phone, string Id)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var RegEmail = (from u in db.AspNetUsers
                            where u.PhoneNumber == phone && u.Id != Id
                            select new { phone }).FirstOrDefault();

            bool status;
            if (RegEmail != null)
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

        //// GET: AspNetUsers/Create
        //public ActionResult Create()
        //{
        //    ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");
        //    return View();
        //}

        //// POST: AspNetUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,idcenter_FK,enabled")] AspNetUser aspNetUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.AspNetUsers.Add(aspNetUser);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", aspNetUser.idcenter_FK);
        //    return View(aspNetUser);
        //}
        [Authorize(Roles = "superadmin,admin")]
        // GET: AspNetUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", aspNetUser.idcenter_FK);

            var userRoles = aspNetUser.AspNetRoles;
            //     var userRoles2 = service.AspNetRoles;
            //   var allRoles = db.AspNetRoles.Where(a => !userRoles.Contains(a));
            //    var allTempRoles = db.AspNetRoles;
            ICollection<AspNetRole> notSelectedRoles = new List<AspNetRole>();
            foreach (var role in db.AspNetRoles)
            {
                if (!userRoles.Contains(role))
                    notSelectedRoles.Add(role);

            }
            var allRoles = notSelectedRoles;
            //ViewBag.RolesID = //new MultiSelectList(db.AspNetRoles, "Id", "Name",service.AspNetRoles.AsEnumerable());
            ViewBag.SelectedRolesId = userRoles;
            ViewBag.NotSelectedRolesId = allRoles;//db.AspNetRoles;
            ViewBag.enableOptions = aspNetUser.enabled;

            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,Email,PasswordHash,PhoneNumber,idcenter_FK,enabled")] AspNetUser aspNetUser, string[] currentSelectedRolesID)//PasswordHash,
        {
            if (ModelState.IsValid)
            {
                AspNetUser s = (from sr in db.AspNetUsers.Include("AspNetRoles")
                                where sr.Id == aspNetUser.Id
                                select sr).FirstOrDefault<AspNetUser>();
                List<AspNetRole> ros = s.AspNetRoles.ToList();
                foreach (var r in ros)
                {
                    s.AspNetRoles.Remove(r);
                    //   await db.SaveChangesAsync();
                    db.SaveChanges();
                }

                if (db.Entry(s).State != EntityState.Detached)
                    db.Entry(s).State = EntityState.Detached;
                db.Entry(aspNetUser).State = EntityState.Modified;

                foreach (var r in db.AspNetRoles)
                    if (currentSelectedRolesID != null)
                    {
                        foreach (string roleId in currentSelectedRolesID)
                        {

                            AspNetRole rls = db.AspNetRoles.Find(roleId);
                            aspNetUser.AspNetRoles.Add(rls);
                        }
                    }
                //(aspNetUser, aspNetUser.PasswordHash);
                ApplicationUserManager usrMngr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      //          IPasswordHasher hash = usrMngr.PasswordHasher;//(aspNetUser, aspNetUser.PasswordHash);
      //          aspNetUser.PasswordHash = hash.HashPassword(aspNetUser.PasswordHash);
                // IdentityResult i =  await usrMngr.UpdateSecurityStampAsync(aspNetUser.Id);
                aspNetUser.SecurityStamp = Guid.NewGuid().ToString();//usrMngr.GetSecurityStamp(aspNetUser.Id);
                await db.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", aspNetUser.idcenter_FK);
            var userRoles = aspNetUser.AspNetRoles;
            //     var userRoles2 = service.AspNetRoles;
            //   var allRoles = db.AspNetRoles.Where(a => !userRoles.Contains(a));
            //    var allTempRoles = db.AspNetRoles;
            ICollection<AspNetRole> notSelectedRoles = new List<AspNetRole>();
            foreach (var role in db.AspNetRoles)
            {
                if (!userRoles.Contains(role))
                    notSelectedRoles.Add(role);

            }
            var allRoles = notSelectedRoles;
            //ViewBag.RolesID = //new MultiSelectList(db.AspNetRoles, "Id", "Name",service.AspNetRoles.AsEnumerable());
            ViewBag.SelectedRolesId = userRoles;
            ViewBag.NotSelectedRolesId = allRoles;//db.AspNetRoles;
            ViewBag.enableOptions = aspNetUser.enabled;
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
        //    if (aspNetUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUser);
        //}

        //// POST: AspNetUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
        //    db.AspNetUsers.Remove(aspNetUser);
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
