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
    [Authorize (Roles = "superadmin,admin,reporter,coEducation,coProfessional,coChildProtection,coPsychologicalSupport,coDayCare,coHomeCare,coSGBV,coSmallProjects,coOutReachTeam,coInkindAssistance,coAwareness")]
    public class ReportingController : Controller
    {
        private sds_dbEntities db = new sds_dbEntities();
        // GET: Reporting
        public ActionResult Index()
        {
            var centers = db.centers;


            ViewBag.centers = centers;

            return View();
        }

        public ActionResult ChartIndex()
        {
            //Highcharts servicechart = ServiceChart();
            //Highcharts personchart = PersonCharts();

            List<Highcharts> publiccharts = new List<Highcharts>();

            if (User.IsInRole("reporter") || User.IsInRole("admin") || User.IsInRole("superadmin"))
            {
                publiccharts.Add(PersonCharts());
                publiccharts.Add(workflow(null));
            }
            else
            {
                if (User.IsInRole("coEducation")) // منسق قسم التعليمي
                {
                    publiccharts.Add(workflow("coEducation"));
                }
                if (User.IsInRole("coProfessional")) // منسق قسم المهني
                {
                    publiccharts.Add(workflow("coProfessional"));
                }

                if (User.IsInRole("coSGBV")) //منسق قسم SGBV
                {
                    publiccharts.Add(workflow("coSGBV"));
                }
                if (User.IsInRole("coChildProtection")) // منسق قسم حماية الطفل
                {
                    publiccharts.Add(workflow("coChildProtection"));
                }

                if (User.IsInRole("coPsychologicalSupport")) // منسق قسم حالة الدعم النفسي
                {
                    publiccharts.Add(workflow("coPsychologicalSupport"));
                }

                if (User.IsInRole("coDayCare")) // منسق قسم الرعاية النهارية
                {
                    publiccharts.Add(workflow("coDayCare"));
                }

                if (User.IsInRole("coHomeCare")) // منسق قسم الرعاية المنزلية
                {
                    publiccharts.Add(workflow("coHomeCare"));
                }

                if (User.IsInRole("coSmallProjects")) // منسق قسم المشاريع الصغيرة
                {
                    publiccharts.Add(workflow("coSmallProjects"));
                }

                if (User.IsInRole("coInkindAssistance")) // منسق قسم المساعدات العينة
                {
                    publiccharts.Add(workflow("coInkindAssistance"));
                }

                if (User.IsInRole("coAwareness")) // منسق قسم التوعية
                {
                    publiccharts.Add(workflow("coInkindAssistance"));
                }
                if (User.IsInRole("coOutReachTeam"))
                {
                    publiccharts.Add(workflow("coOutReachTeam"));
                }

                
            }


                

            var charts = new ChartsViewModel
            {
                publiccharts = publiccharts

            };
            return View(charts);
        }

        public ActionResult GetCounts(DateTime? startDate, DateTime? endDate, string centerid)
        {
            List<ReportingViewModel> servicereportlist = new List<ReportingViewModel>();
            List<ReportingViewModel> threeaddresreportlist = null;
            List<ReportingViewModel> threewaysreportlist = null;
            List<ReportingViewModel> weaknesreportlist = null;

            string centername;
            if (centerid != "all")
                centername = db.centers.SingleOrDefault(c => c.idcenter == centerid).name;
            else
                centername = "كل المشروع";

            if ( User.IsInRole("reporter") || User.IsInRole("admin") || User.IsInRole("superadmin"))
            {
                threeaddresreportlist = new List<ReportingViewModel>();
                threewaysreportlist = new List<ReportingViewModel>();
                weaknesreportlist = new List<ReportingViewModel>();

                servicereportlist = getservicereport(startDate, endDate, centerid);
                threeaddresreportlist = getthreeaddress(startDate, endDate, centerid);
                threewaysreportlist = getthreeways(startDate, endDate, centerid);
                weaknesreportlist = getweakness(startDate, endDate, centerid);
            }

            else
            {
                servicereportlist = getservicereport(startDate, endDate, centerid);
            }

            var result = new { name = centername, servicerport = servicereportlist, weaknesreport = weaknesreportlist, threewaysreport = threewaysreportlist, threeaddresreport = threeaddresreportlist };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        private List<ReportingViewModel> getservicereport(DateTime? startDate, DateTime? endDate, string centerid)
        {
            List<ReportingViewModel> report = new List<ReportingViewModel>();
            ReportingViewModel newregisterreport;
            List<ReportingViewModel> servicesreport;

            if ( User.IsInRole("reporter") || User.IsInRole("admin") || User.IsInRole("superadmin"))
            {
                if (centerid == "all")
                {
                    var people = db.people.Where(p => p.registrationdate >= startDate && p.registrationdate <= endDate);
                    newregisterreport = new ReportingViewModel()
                    {
                        type = "أ. نظرة عامة إحصائية",
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


                    servicesreport = (from p in db.people
                                      join rp in db.referalpersons
                                      on p.idperson equals rp.idperson_FK
                                      join s in db.services
                                      on rp.idservice_FK equals s.idservice
                                      where rp.referaldate >= startDate && rp.referaldate <= endDate
                                      group rp by new
                                      {
                                          s.name,

                                      } into rep
                                      select new ReportingViewModel
                                      {
                                          type = "ب.الخدمات",
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

                }

                else
                {
                    var people = db.people.Where(p => p.registrationdate >= startDate && p.registrationdate <= endDate && p.idcenter_FK == centerid);
                    newregisterreport = new ReportingViewModel()
                    {
                        type = "أ. نظرة عامة إحصائية",
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


                    servicesreport = (from p in db.people
                                      join rp in db.referalpersons
                                      on p.idperson equals rp.idperson_FK
                                      join s in db.services
                                      on rp.idservice_FK equals s.idservice
                                      where rp.referaldate >= startDate && rp.referaldate <= endDate && p.idcenter_FK == centerid
                                      group rp by new
                                      {
                                          s.name,

                                      } into rep
                                      select new ReportingViewModel
                                      {
                                          type = "ب.الخدمات",
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
                }

                report.Add(newregisterreport);
                foreach (var item in servicesreport)
                {
                    report.Add(item);
                }
            }

           if ( User.IsInRole("coEducation")) // منسق قسم التعليمي
            {
                report = getcordinatorreport("coEducation", centerid, startDate, endDate);
            }
           if ( User.IsInRole("coProfessional")) // منسق قسم المهني
            {
                report = getcordinatorreport("coProfessional", centerid, startDate, endDate);
            }
            
            if (User.IsInRole("coSGBV")) //منسق قسم SGBV
            {
                report = getcordinatorreport("coSGBV", centerid, startDate, endDate);
            }
            if (User.IsInRole("coChildProtection")) // منسق قسم حماية الطفل
            {
                report = getcordinatorreport("coChildProtection", centerid, startDate, endDate);
            }

            if (User.IsInRole("coPsychologicalSupport")) // منسق قسم حالة الدعم النفسي
            {
                report = getcordinatorreport("coPsychologicalSupport", centerid, startDate, endDate);
            }

            if (User.IsInRole("coDayCare")) // منسق قسم الرعاية النهارية
            {
                report = getcordinatorreport("coDayCare", centerid, startDate, endDate);
            }

            if (User.IsInRole("coHomeCare")) // منسق قسم الرعاية المنزلية
            {
                report = getcordinatorreport("coHomeCare", centerid, startDate, endDate);
            }

            if (User.IsInRole("coSmallProjects")) // منسق قسم المشاريع الصغيرة
            {
                report = getcordinatorreport("coSmallProjects", centerid, startDate, endDate);
            }

            if (User.IsInRole("coInkindAssistance")) // منسق قسم المساعدات العينة
            {
                report = getcordinatorreport("coInkindAssistance", centerid, startDate, endDate);
            }

            if (User.IsInRole("coAwareness")) // منسق قسم التوعية
            {
                report = getcordinatorreport("coAwareness", centerid, startDate, endDate);
            }
            if (User.IsInRole("coOutReachTeam"))
            {
                report = getcordinatorreport("coOutReachTeam", centerid, startDate, endDate);
            }



            return report;
        }

        public List<ReportingViewModel> getcordinatorreport(string role ,string centerid , DateTime? startDate, DateTime? endDate)
        {
            List<ReportingViewModel> servicesreport;
            int? idcase = db.AspNetRoles.SingleOrDefault(r => r.Name == role).idcase;
            IQueryable<service> servicesofroles = db.services.Where(s => s.idcase_FK == idcase);

            if (centerid == "all")
            {
                servicesreport = (from p in db.people
                                  join rp in db.referalpersons
                                  on p.idperson equals rp.idperson_FK
                                  join s in servicesofroles
                                  on rp.idservice_FK equals s.idservice
                                  where rp.referaldate >= startDate && rp.referaldate <= endDate
                                  group rp by new
                                  {
                                      s.name,

                                  } into rep
                                  select new ReportingViewModel
                                  {
                                      type = "ب.الخدمات",
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
            }
            else
            {
                servicesreport = (from p in db.people
                                  join rp in db.referalpersons
                                  on p.idperson equals rp.idperson_FK
                                  join s in servicesofroles
                                  on rp.idservice_FK equals s.idservice
                                  where rp.referaldate >= startDate && rp.referaldate <= endDate && p.idcenter_FK == centerid
                                  group rp by new
                                  {
                                      s.name,

                                  } into rep
                                  select new ReportingViewModel
                                  {
                                      type = "ب.الخدمات",
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

            }

            return servicesreport;
        }



        private List<ReportingViewModel> getthreeaddress(DateTime? startDate, DateTime? endDate, string centerid)
        {
            List<ReportingViewModel> addresses;
            if (centerid == "all")
            {
                addresses = (from p in db.people
                             where p.registrationdate >= startDate && p.registrationdate <= endDate
                             group p by new
                             {
                                 p.family.currentaddress_details
                             } into percount
                             select new ReportingViewModel
                             {
                                 addressname = percount.Key.currentaddress_details,
                                 count = percount.Count()
                             }).ToList();
            }
            else
            {
                addresses = (from p in db.people
                             where p.registrationdate >= startDate && p.registrationdate <= endDate && p.idcenter_FK == centerid
                             group p by new
                             {
                                 p.family.currentaddress_details
                             } into percount
                             select new ReportingViewModel
                             {
                                 addressname = percount.Key.currentaddress_details,
                                 count = percount.Count()
                             }).ToList();
            }



            return addresses;
        }

        private List<ReportingViewModel> getthreeways(DateTime? startDate, DateTime? endDate, string centerid)
        {
            List<ReportingViewModel> ways;
            if (centerid == "all")
            {
                ways = (from p in db.people
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
            }
            else
            {
                ways = (from p in db.people
                        join pm in db.personmanages
                        on p.idperson equals pm.idperson_FK
                        join ml in db.managelists
                        on pm.idmanagelist_FK equals ml.idmanagelist

                        where p.registrationdate >= startDate && p.registrationdate <= endDate && p.idcenter_FK == centerid && ml.flag == "KC"
                        group p by new
                        {
                            ml.name
                        } into percount
                        select new ReportingViewModel
                        {
                            waysknow = percount.Key.name,
                            count = percount.Count()
                        }).ToList();
            }


            return ways;
        }

        private List<ReportingViewModel> getweakness(DateTime? startDate, DateTime? endDate, string centerid)
        {
            // var personwithservicestat;
            List<ReportingViewModel> weakness;

            if (centerid == "all")
            {
                var personwithservicestate = from rp in db.referalpersons
                                             join p in db.people
                                            on rp.idperson_FK equals p.idperson
                                             select new
                                             {
                                                 idperson = p.idperson,
                                                 age = p.age,
                                                 gender = p.gender,
                                                 familynature = p.family.familynature,
                                                 idreferal = rp.idreferalperson,
                                                 idservice = rp.idservice_FK,
                                                 referalstate = rp.referalstate,
                                                 servicestate = rp.servicestate,
                                                 referaldate = rp.referaldate

                                             };
                weakness = (from p in personwithservicestate
                            join pm in db.personmanages
                            on p.idperson equals pm.idperson_FK
                            join ml in db.managelists
                            on pm.idmanagelist_FK equals ml.idmanagelist
                            where p.referaldate >= startDate && p.referaldate <= endDate && (ml.flag.Length > 1 && ml.flag.StartsWith("W")) // هذا يعني أنه فقط نقاط الضعف لأنو عطول بتبلش بحرف دبليو وبتكون أكتر من محرف
                            group p by new
                            {
                                ml.name
                            } into rep
                            select new ReportingViewModel
                            {
                                type = "ج. نقاط الضعف",
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
                                internaldisplacemenmalecount = rep.Where(g => g.familynature == "نازح داخلي" && g.gender == "ذكر").Count(),
                                internaldisplacemenfemalecount = rep.Where(g => g.familynature == "نازح داخلي" && g.gender == "أنثى").Count(),
                                hostcommunirtmalecount = rep.Where(g => g.familynature == "فرد من المجتمع المضيف" && g.gender == "ذكر").Count(),
                                hostcommunirtfemalecount = rep.Where(g => g.familynature == "فرد من المجتمع المضيف" && g.gender == "أنثى").Count(),
                                internaldisplacedreturneemalecount = rep.Where(g => g.familynature == "نازح داخلي عائد" && g.gender == "ذكر").Count(),
                                internaldisplacedreturnefeemalecount = rep.Where(g => g.familynature == "نازح داخلي عائد" && g.gender == "أنثى").Count(),
                                refugeereturningtosyriamalecount = rep.Where(g => g.familynature == "لاجئ عائد إلى سورية" && g.gender == "ذكر").Count(),
                                refugeereturningtosyriafemalecount = rep.Where(g => g.familynature == "لاجئ عائد إلى سورية" && g.gender == "أنثى").Count(),
                                refugeewantedmalecount = rep.Where(g => g.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.gender == "ذكر").Count(),
                                refugeewantedfemalecount = rep.Where(g => g.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.gender == "أنثى").Count(),
                                inprogressstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.gender == "ذكر").Count(),
                                inprogressstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.gender == "أنثى").Count(),
                                closedstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.gender == "ذكر").Count(),
                                closedstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.gender == "أنثى").Count()
                            }).ToList();
            }
            else
            {
                var personwithservicestate = from rp in db.referalpersons
                                             join p in db.people
                                            on rp.idperson_FK equals p.idperson
                                             where p.idcenter_FK == centerid
                                             select new
                                             {
                                                 idperson = p.idperson,
                                                 age = p.age,
                                                 gender = p.gender,
                                                 familynature = p.family.familynature,
                                                 idreferal = rp.idreferalperson,
                                                 idservice = rp.idservice_FK,
                                                 referalstate = rp.referalstate,
                                                 servicestate = rp.servicestate,
                                                 referaldate = rp.referaldate

                                             };
                weakness = (from p in personwithservicestate
                            join pm in db.personmanages
                            on p.idperson equals pm.idperson_FK
                            join ml in db.managelists
                            on pm.idmanagelist_FK equals ml.idmanagelist
                            where p.referaldate >= startDate && p.referaldate <= endDate && (ml.flag.Length > 1 && ml.flag.StartsWith("W")) // هذا يعني أنه فقط نقاط الضعف لأنو عطول بتبلش بحرف دبليو وبتكون أكتر من محرف
                            group p by new
                            {
                                ml.name
                            } into rep
                            select new ReportingViewModel
                            {
                                type = "ج. نقاط الضعف",
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
                                internaldisplacemenmalecount = rep.Where(g => g.familynature == "نازح داخلي" && g.gender == "ذكر").Count(),
                                internaldisplacemenfemalecount = rep.Where(g => g.familynature == "نازح داخلي" && g.gender == "أنثى").Count(),
                                hostcommunirtmalecount = rep.Where(g => g.familynature == "فرد من المجتمع المضيف" && g.gender == "ذكر").Count(),
                                hostcommunirtfemalecount = rep.Where(g => g.familynature == "فرد من المجتمع المضيف" && g.gender == "أنثى").Count(),
                                internaldisplacedreturneemalecount = rep.Where(g => g.familynature == "نازح داخلي عائد" && g.gender == "ذكر").Count(),
                                internaldisplacedreturnefeemalecount = rep.Where(g => g.familynature == "نازح داخلي عائد" && g.gender == "أنثى").Count(),
                                refugeereturningtosyriamalecount = rep.Where(g => g.familynature == "لاجئ عائد إلى سورية" && g.gender == "ذكر").Count(),
                                refugeereturningtosyriafemalecount = rep.Where(g => g.familynature == "لاجئ عائد إلى سورية" && g.gender == "أنثى").Count(),
                                refugeewantedmalecount = rep.Where(g => g.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.gender == "ذكر").Count(),
                                refugeewantedfemalecount = rep.Where(g => g.familynature == "لاجئ أو طالب لجوء من دولة أخرى" && g.gender == "أنثى").Count(),
                                inprogressstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.gender == "ذكر").Count(),
                                inprogressstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "In prgress" && g.gender == "أنثى").Count(),
                                closedstatemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.gender == "ذكر").Count(),
                                closedstatefemalecount = rep.Where(g => g.referalstate == "Approved" && g.servicestate == "Closed" && g.gender == "أنثى").Count()
                            }).ToList();
            }


            return weakness;
        }




        public Highcharts PersonCharts()
        {
            List<ReportingViewModel> query = (from c in db.centers
                                              join p in db.people
                                              on c.idcenter equals p.idcenter_FK
                                              group c by c.name into rep
                                              select new ReportingViewModel
                                              {
                                                  servicename = rep.Key,
                                                  count = rep.Count()
                                              }).ToList();


            string[] keys = new string[query.Count];
            object[] counts = new object[query.Count];

            for (int i = 0; i < query.Count; i++)
            {
                keys[i] = query[i].servicename;
                counts[i] = (object)query[i].count;
            }



            Highcharts chart = new Highcharts("personchart")
                .InitChart(new Chart { DefaultSeriesType = DotNet.Highcharts.Enums.ChartTypes.Column })
                .SetXAxis(new XAxis { Categories = keys, Title = new XAxisTitle { Text = " أسماء المراكز", Align = DotNet.Highcharts.Enums.AxisTitleAligns.Middle } })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "الأعداد" } })
                .SetSeries(new Series { Data = new Data(counts), Color = System.Drawing.Color.Green, Name = "المركز" })
                .SetTitle(new Title { Text = "أعداد المسجلين بالنسبة للمراكز" })
                .SetLoading(new Loading { HideDuration = 10, LabelStyle = "", Style = "" });

            //.SetExportinVg();
            return chart;
        }


        public Highcharts workflow(string role)
        {

            List<service> services;

            if ( role == null)// this means for all services
            {
                services = db.services.ToList();
            }
            else
            {
                int? idcase = db.AspNetRoles.SingleOrDefault(r => r.Name == role).idcase;
                services = db.services.Where(s => s.idcase_FK == idcase).ToList();
            }
          
                

            string[] publickeys = new string[0];
            List<Series> servicesseries = new List<Series>();

            foreach (var item in services)
            {
                List<ReportingViewModel> query = (from s in db.services
                                                  join rp in db.referalpersons
                                                  on s.idservice equals rp.idservice_FK
                                                  where s.name == item.name && rp.referalstate == "Approved" && rp.servicestate == "Closed"
                                                  group s by new
                                                  {
                                                      month = rp.referaldate.Value.Month,
                                                      year = rp.referaldate.Value.Year
                                                  } into rep
                                                  select new ReportingViewModel
                                                  {
                                                      servicename = rep.Key.year + " - " + rep.Key.month,
                                                      count = rep.Count()
                                                  }).ToList();

                string[] keys = new string[query.Count];
                object[] counts = new object[query.Count];

                for (int i = 0; i < query.Count; i++)
                {
                    keys[i] = query[i].servicename;
                    counts[i] = (object)query[i].count;
                }

                if (keys.Length > publickeys.Length)
                {
                    publickeys = new string[keys.Length];
                    for (int i = 0; i < keys.Length; i++)
                        publickeys[i] = keys[i];
                }



                Series series = new Series { Data = new Data(counts), Name = item.name };
                servicesseries.Add(series);
            }



            Highcharts chart = new Highcharts("workflowchart")
               .InitChart(new Chart { DefaultSeriesType = DotNet.Highcharts.Enums.ChartTypes.Line })
               .SetXAxis(new XAxis { Categories = publickeys, Title = new XAxisTitle { Text = "الأشهر", Align = DotNet.Highcharts.Enums.AxisTitleAligns.Middle } })
               .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "الأعداد" } })
               .SetSeries(servicesseries.ToArray())
               .SetTitle(new Title { Text = "المخطط الخطي لسير العمل ضمن المشروع" })
               .SetLoading(new Loading { HideDuration = 10, LabelStyle = "", Style = "" });

            return chart;
        }

       



    }
}