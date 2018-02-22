using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using SDS_SanadDistributedSystem.Models;
using SDS_SanadDistributedSystem.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDS_SanadDistributedSystem.Controllers
{
   
    public class ReportingController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        // GET: Reporting
        public ActionResult Index()
        {
            // إجمالي
            List<ReportingViewModel> totallist = db.people.GroupBy(p => p.gender).Select(p => new ReportingViewModel { gender = p.Key, count = p.Count() }).ToList();
            ViewBag.total = totallist;
            
            // الفئات العمرية +  الجنس
            List<ReportingViewModel> lessthan = (from p in db.people
                              where p.age < 18
                                group p by new  
                                {
                                    p.gender,
                                } into percount
                                select new ReportingViewModel
                                {
                                    age = "< 18",
                                    gender = percount.Key.gender,
                                    count = percount.Count()
                                }).ToList();

            List<ReportingViewModel> between = (from p in db.people
                              where p.age >= 18 && p.age  < 60
                              group p by new
                              {
                                  p.gender,
                              } into percount
                              select new ReportingViewModel
                              {
                                  age = "18 - 59",
                                  gender = percount.Key.gender,
                                  count = percount.Count()
                              }).ToList();

            List<ReportingViewModel> heigherthan = (from p in db.people
                              where p.age >= 60
                              group p by new
                              {
                                  p.gender,
                              } into percount
                              select new ReportingViewModel
                              {
                                  age = ">= 60",
                                  gender = percount.Key.gender,
                                  count = percount.Count()
                              }).ToList();

            List<ReportingViewModel> agegender = new List<ReportingViewModel>();
            foreach (var item in lessthan)
                agegender.Add(item);

            foreach (var item in between)
                agegender.Add(item);

            foreach (var item in heigherthan)
                agegender.Add(item);

            ViewBag.totalagegender = agegender;

            // طبيعة العائلة + الجنس
            List<ReportingViewModel> naturegender = (from p in db.people
                                                    where p.age >= 60
                                                    group p by new
                                                    {
                                                        p.family.familynature,
                                                        p.gender,
                                                    } into percount
                                                    select new ReportingViewModel
                                                    {
                                                        familynature = percount.Key.familynature,
                                                        gender = percount.Key.gender,
                                                        count = percount.Count()
                                                    }).ToList();

            ViewBag.totalnaturegender = naturegender;


            //ViewBag.totalcounts = r1;

            //int size = r1.Count();
            //string[] keys = new string[size];
            //object[] counts = new object[size];
            //int i = 0;
            //foreach (var item in r1)
            //{
            //    keys[i] = item.age + " " + item.gender + " " + item.familynature;
            //    counts[i] = item.count;
            //    i++;
            //}

            ViewBag.servicesreport = (from p in db.people
                                     join rp in db.referalpersons
                                     on p.idperson equals rp.idperson_FK
                                     join s in db.services
                                     on rp.idservice_FK equals s.idservice
                                     group p by new
                                     {
                                         s.name,

                                     } into rep
                                     select new ReportingViewModel
                                     {
                                         servicename = rep.Key.name,
                                         count = rep.Count(),
                                         malecount = rep.Where(g => g.gender == "ذكر").Count(),
                                         femalecount = rep.Where(g => g.gender == "أنثى").Count(),
                                         lesseighteenmalecount = rep.Where(g => g.age < 18 && g.gender == "ذكر").Count(),
                                         lesseighteenfemalecount = rep.Where(g => g.age < 18 && g.gender == "أنثى").Count(),
                                         betweenmalecount = rep.Where(g => g.age >= 18 && g.age < 60 && g.gender == "ذكر").Count(),
                                         betweenfemalecount = rep.Where(g => g.age >= 18 && g.age < 60 && g.gender == "أنثى").Count(),
                                         heighersixtymalecount = rep.Where(g => g.age >= 60 && g.gender == "ذكر").Count(),
                                         heighersixtyfemalecount = rep.Where(g => g.age >= 60 && g.gender == "أنثى").Count(),
                                         internaldisplacemenmalecount = rep.Where(g => g.family.familynature == "نازح داخلي" && g.gender == "ذكر").Count(),
                                         internaldisplacemenfemalecount = rep.Where(g => g.family.familynature == "نازح داخلي" && g.gender == "أنثى").Count(),
                                         hostcommunirtmalecount = rep.Where(g => g.family.familynature == "فرد من المجتمع المضيف" && g.gender == "ذكر").Count(),
                                         hostcommunirtfemalecount = rep.Where(g => g.family.familynature == "فرد من المجتمع المضيف" && g.gender == "أنثى").Count(),
                                         internaldisplacedreturneemalecount = rep.Where(g => g.family.familynature == "نازح داخلي عائد" && g.gender == "ذكر").Count(),
                                         internaldisplacedreturnefeemalecount = rep.Where(g => g.family.familynature == "نازح داخلي عائد" && g.gender == "أنثى").Count(),
                                         refugeereturningtosyriamalecount = rep.Where(g => g.family.familynature == "لاجئ عائد إلى سورية" && g.gender == "ذكر").Count(),
                                         refugeereturningtosyriafemalecount = rep.Where(g => g.family.familynature == "لاجئ عائد إلى سورية" && g.gender == "أنثى").Count(),
                                         refugeewantedmalecount = rep.Where(g => g.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.gender == "ذكر").Count(),
                                         refugeewantedfemalecount = rep.Where(g => g.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.gender == "أنثى").Count(),
                                         //inprogressstatecount = rep.Where(g => g.referalpersons.Where(r => r.referalstate == "Approved" && r.servicestate == "In prgress")).Count(),
                                         //closedstatecount = rep.Where(g => g.referalpersons.Where(r => r.referalstate == "Approved" && r.servicestate == "Closed")).Count()

                                     }).ToList();



            //Highcharts chart = new Highcharts("chart")
            //    .SetXAxis(new XAxis { Categories = keys })
            //    .SetSeries(new Series { Data = new Data(counts) });
            //return View(chart);
            return View();
        }

        public ActionResult GetCounts(DateTime? startDate, DateTime? endDate, string submit)
        {
            List<ReportingViewModel> reportlist = new List<ReportingViewModel>();

            switch(submit)
            {
                case "عدد المسجلين الجدد":
                    reportlist = getcounts(startDate, endDate);
                    break;
                case "أكثر ثلاث عناوين مكررة":
                    reportlist = getthreeaddress(startDate, endDate);
                    break;
                case "أكثر ثلاث طرق للتعرف  على المركز المجتمعي":
                    reportlist = getthreeways(startDate, endDate);
                    break;
            }

            var result = new { submitname = submit, resultus = reportlist };
            return Json(result, JsonRequestBehavior.AllowGet);

            //return new JsonResult { Data = reportlist , JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //return View();
        }

        private List<ReportingViewModel> getcounts(DateTime? startDate, DateTime? endDate)
        {
            List<ReportingViewModel> totalcounts = (from p in db.people
                                                    where p.registrationdate >= startDate && p.registrationdate <= endDate
                                                    group p by new
                                                    {
                                                        age = p.age / 10,
                                                        p.gender,
                                                        p.family.familynature
                                                    } into percount
                                                    select new ReportingViewModel
                                                    {
                                                        familynature = percount.Key.familynature,
                                                        gender = percount.Key.gender,
                                                        age = "",
                                                        count = percount.Count()
                                                    }).ToList();
            return totalcounts;
        }

        private List<ReportingViewModel> getthreeaddress(DateTime? startDate, DateTime? endDate)
        {
            List<ReportingViewModel> addresses = (from p in db.people
                                                    where p.registrationdate >= startDate && p.registrationdate <= endDate
                                                    group p by new
                                                    {
                                                       p.currentaddress
                                                    } into percount
                                                    select new ReportingViewModel
                                                    {
                                                        addressname = percount.Key.currentaddress,
                                                        count = percount.Count()
                                                    }).ToList();
            return addresses;
        }

        private List<ReportingViewModel> getthreeways(DateTime? startDate, DateTime? endDate)
        {
            List<ReportingViewModel> ways = (from p in db.people
                                             join pm in db.personmanages
                                             on p.idperson equals pm.idperson_FK
                                             join ml in db.managelists
                                             on pm.idmanagelist_FK equals ml.idmanagelist

                                             where p.registrationdate >= startDate && p.registrationdate <= endDate && ml.flag == "KC"
                                            group p by new
                                            {
                                              ml.name
                                            } into percount
                                            select new ReportingViewModel
                                            {
                                                waysknow = percount.Key.name,
                                                count = percount.Count()
                                            }).ToList();
            return ways;
        }
    }
}