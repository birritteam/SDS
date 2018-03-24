using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SDS_SanadDistributedSystem.Models;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class BaseController : Controller
    {
        //public static int CountNotificationOffilicn = 0;
        private sds_dbEntities db = new sds_dbEntities();
        // GET: Base
        public BaseController()
        {
            ViewBag.ServicesBar = db.services.Include(s=>s.centerservices).Where(u => u.centerservices.Any(s => s.idservice_FK == u.idservice && s.enabled )).ToList();

            //if (CountNotificationOffilicn != 0)
            //{
            //    ViewBag.CountNotficationOffline = CountNotificationOffilicn;
            //}
        }
    }
}