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
           
            List<ReportingViewModel> servicesreport = (from p in db.people
                                  join rp in db.referalpersons
                                  on p.idperson equals rp.idperson_FK
                                  join s in db.services
                                  on rp.idservice_FK equals s.idservice
                                  group rp by new
                                  {
                                      s.name,

                                  } into rep
                                  select new ReportingViewModel
                                  {
                                      servicename = rep.Key.name,
                                      count = rep.Count(),
                                      malecount = rep.Where(g => g.person.gender == "ذكر").Count(),
                                      femalecount = rep.Where(g => g.person.gender == "أنثى").Count(),
                                      lesseighteenmalecount = rep.Where(g => g.person.age < 18 && g.person.gender == "ذكر").Count(),
                                      lesseighteenfemalecount = rep.Where(g => g.person.age < 18 && g.person.gender == "أنثى").Count(),
                                      betweenmalecount = rep.Where(g => g.person.age >= 18 && g.person.age < 60 && g.person.gender == "ذكر").Count(),
                                      betweenfemalecount = rep.Where(g => g.person.age >= 18 && g.person.age < 60 && g.person.gender == "أنثى").Count(),
                                      heighersixtymalecount = rep.Where(g => g.person.age >= 60 && g.person.gender == "ذكر").Count(),
                                      heighersixtyfemalecount = rep.Where(g => g.person.age >= 60 && g.person.gender == "أنثى").Count(),
                                      internaldisplacemenmalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي" && g.person.gender == "ذكر").Count(),
                                      internaldisplacemenfemalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي" && g.person.gender == "أنثى").Count(),
                                      hostcommunirtmalecount = rep.Where(g => g.person.family.familynature == "فرد من المجتمع المضيف" && g.person.gender == "ذكر").Count(),
                                      hostcommunirtfemalecount = rep.Where(g => g.person.family.familynature == "فرد من المجتمع المضيف" && g.person.gender == "أنثى").Count(),
                                      internaldisplacedreturneemalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي عائد" && g.person.gender == "ذكر").Count(),
                                      internaldisplacedreturnefeemalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي عائد" && g.person.gender == "أنثى").Count(),
                                      refugeereturningtosyriamalecount = rep.Where(g => g.person.family.familynature == "لاجئ عائد إلى سورية" && g.person.gender == "ذكر").Count(),
                                      refugeereturningtosyriafemalecount = rep.Where(g => g.person.family.familynature == "لاجئ عائد إلى سورية" && g.person.gender == "أنثى").Count(),
                                      refugeewantedmalecount = rep.Where(g => g.person.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.person.gender == "ذكر").Count(),
                                      refugeewantedfemalecount = rep.Where(g => g.person.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.person.gender == "أنثى").Count(),
                                      inprogressstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.person.gender == "ذكر").Count(),
                                      inprogressstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.person.gender == "أنثى").Count(),
                                      closedstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.person.gender == "ذكر").Count(),
                                      closedstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.person.gender == "أنثى").Count()
                                  }).ToList();



            //Highcharts chart = new Highcharts("chart")
            //    .SetXAxis(new XAxis { Categories = keys })
            //    .SetSeries(new Series { Data = new Data(counts) });
            //return View(chart);
            return View(servicesreport);
        }

        public ActionResult GetCounts(DateTime? startDate, DateTime? endDate, string submit)
        {
            List<ReportingViewModel> reportlist = new List<ReportingViewModel>();

            switch(submit)
            {
                case "إعداد التقرير الشهري":
                    reportlist = getservicereport(startDate, endDate);
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

        private List<ReportingViewModel> getservicereport(DateTime? startDate, DateTime? endDate)
        {
            var people = db.people.Where(p => p.registrationdate >= startDate && p.registrationdate <= endDate);
            ReportingViewModel newregisterreport = new ReportingViewModel()
            {
                servicename = "تسجيل جديد",
                count = people.Count(),
                malecount = people.Where(p => p.gender == "ذكر").Count(),
                femalecount = people.Where(p => p.gender == "أنثى").Count(),
                lesseighteenmalecount = people.Where(p => p.gender == "ذكر" && p.age < 18).Count(),
                lesseighteenfemalecount = people.Where(p => p.gender == "أنثى" && p.age < 18).Count(),
                betweenmalecount = people.Where(p => p.gender == "ذكر" && p.age >= 18 && p.age < 60).Count(),
                betweenfemalecount = people.Where(p => p.gender == "أنثى" && p.age >= 18 && p.age < 60).Count(),
                heighersixtymalecount = people.Where(p => p.gender == "ذكر" && p.age >= 60).Count(),
                heighersixtyfemalecount = people.Where(p => p.gender == "أنثى" && p.age >= 60).Count(),
                internaldisplacemenmalecount = people.Where(p => p.family.familynature == "نازح داخلي" && p.gender == "ذكر").Count(),
                internaldisplacemenfemalecount = people.Where(p => p.family.familynature == "نازح داخلي" && p.gender == "أنثى").Count(),
                hostcommunirtmalecount = people.Where(p => p.family.familynature == "فرد من المجتمع المضيف" && p.gender == "ذكر").Count(),
                hostcommunirtfemalecount = people.Where(p => p.family.familynature == "فرد من المجتمع المضيف" && p.gender == "أنثى").Count(),
                internaldisplacedreturneemalecount = people.Where(p => p.family.familynature == "نازح داخلي عائد" && p.gender == "ذكر").Count(),
                internaldisplacedreturnefeemalecount = people.Where(p => p.family.familynature == "نازح داخلي عائد" && p.gender == "أنثى").Count(),
                refugeereturningtosyriamalecount = people.Where(p => p.family.familynature == "لاجئ عائد إلى سورية" && p.gender == "ذكر").Count(),
                refugeereturningtosyriafemalecount = people.Where(p => p.family.familynature == "لاجئ عائد إلى سورية" && p.gender == "أنثى").Count(),
                refugeewantedmalecount = people.Where(p => p.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && p.gender == "ذكر").Count(),
                refugeewantedfemalecount = people.Where(p => p.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && p.gender == "أنثى").Count(),
                inprogressstatemalecount = null,
                inprogressstatefemalecount = null,
                closedstatemalecount = null,
                closedstatefemalecount = null
            };
          

            List<ReportingViewModel> servicesreport = (from p in db.people
                                                       join rp in db.referalpersons
                                                       on p.idperson equals rp.idperson_FK
                                                       join s in db.services
                                                       on rp.idservice_FK equals s.idservice
                                                       where rp.servicestartdate >= startDate && rp.serviceenddate <= endDate
                                                       group rp by new
                                                       {
                                                           s.name,

                                                       } into rep
                                                       select new ReportingViewModel
                                                       {
                                                           servicename = rep.Key.name,
                                                           count = rep.Count(),
                                                           malecount = rep.Where(g => g.person.gender == "ذكر").Count(),
                                                           femalecount = rep.Where(g => g.person.gender == "أنثى").Count(),
                                                           lesseighteenmalecount = rep.Where(g => g.person.age < 18 && g.person.gender == "ذكر").Count(),
                                                           lesseighteenfemalecount = rep.Where(g => g.person.age < 18 && g.person.gender == "أنثى").Count(),
                                                           betweenmalecount = rep.Where(g => g.person.age >= 18 && g.person.age < 60 && g.person.gender == "ذكر").Count(),
                                                           betweenfemalecount = rep.Where(g => g.person.age >= 18 && g.person.age < 60 && g.person.gender == "أنثى").Count(),
                                                           heighersixtymalecount = rep.Where(g => g.person.age >= 60 && g.person.gender == "ذكر").Count(),
                                                           heighersixtyfemalecount = rep.Where(g => g.person.age >= 60 && g.person.gender == "أنثى").Count(),
                                                           internaldisplacemenmalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي" && g.person.gender == "ذكر").Count(),
                                                           internaldisplacemenfemalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي" && g.person.gender == "أنثى").Count(),
                                                           hostcommunirtmalecount = rep.Where(g => g.person.family.familynature == "فرد من المجتمع المضيف" && g.person.gender == "ذكر").Count(),
                                                           hostcommunirtfemalecount = rep.Where(g => g.person.family.familynature == "فرد من المجتمع المضيف" && g.person.gender == "أنثى").Count(),
                                                           internaldisplacedreturneemalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي عائد" && g.person.gender == "ذكر").Count(),
                                                           internaldisplacedreturnefeemalecount = rep.Where(g => g.person.family.familynature == "نازح داخلي عائد" && g.person.gender == "أنثى").Count(),
                                                           refugeereturningtosyriamalecount = rep.Where(g => g.person.family.familynature == "لاجئ عائد إلى سورية" && g.person.gender == "ذكر").Count(),
                                                           refugeereturningtosyriafemalecount = rep.Where(g => g.person.family.familynature == "لاجئ عائد إلى سورية" && g.person.gender == "أنثى").Count(),
                                                           refugeewantedmalecount = rep.Where(g => g.person.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.person.gender == "ذكر").Count(),
                                                           refugeewantedfemalecount = rep.Where(g => g.person.family.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.person.gender == "أنثى").Count(),
                                                           inprogressstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.person.gender == "ذكر").Count(),
                                                           inprogressstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.person.gender == "أنثى").Count(),
                                                           closedstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.person.gender == "ذكر").Count(),
                                                           closedstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.person.gender == "أنثى").Count()
                                                       }).ToList();

            List<ReportingViewModel> report = new List<ReportingViewModel>();
            report.Add(newregisterreport);
            foreach(var item in servicesreport)
            {
                report.Add(item);
            }
            return report;
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