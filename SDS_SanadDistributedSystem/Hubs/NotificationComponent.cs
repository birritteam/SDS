using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDS_SanadDistributedSystem.Models;

namespace SDS_SanadDistributedSystem.Hubs
{
    public class NotificationComponent
    {
        private sds_dbEntities db = new sds_dbEntities();
        public List<referalperson> GetReferals(DateTime afterDate, string usertosend)
        {
            var list = db.referalpersons.Where(a => a.submittingdate <= afterDate && a.referalreicver_FK == usertosend && a.referalstate == "Pending" && a.servicestate == "Pending").OrderByDescending(a => a.submittingdate).ToList();
            return list;
        }

        public List<temporal> GetTemporal(DateTime afterDate, string usertosend)
        {
            var list = db.temporals.Where(a => a.submittingdate <= afterDate && a.referalreicver_FK == usertosend && a.referalstate == "Pending" && a.servicestate == "Pending").OrderByDescending(a => a.submittingdate).ToList();
            return list;
        }
    }

    
}