using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDS_SanadDistributedSystem.Models;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class BaseController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        // GET: Base
        public BaseController()
        {
            ViewBag.ServicesBar = db.services.ToList();
        }
    }
}