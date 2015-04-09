using Asn_23.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asn_23.Controllers
{
    [Authorize(Roles = "Administrator,Reporter,Worker")]
    public class MVCReportController : Controller
    {
        // GET: MVCReport
        public ActionResult Index()
        {
            GoodSamaritanContext ctx = new GoodSamaritanContext();

            IEnumerable<SelectListItem> siYears = (from c in ctx.FiscalYears
                                                   select new { c.FiscalYearId, c.Years }).AsEnumerable()
                                                   .Select(m => new SelectListItem()
                                                   {
                                                       Value = m.FiscalYearId.ToString(),
                                                       Text = m.Years
                                                   });

            List<dynamic> siMonths = new List<dynamic>();
            siMonths.Add(new SelectListItem() { Value = "1", Text = "January" });
            siMonths.Add(new SelectListItem() { Value = "2", Text = "February" });
            siMonths.Add(new SelectListItem() { Value = "3", Text = "March" });
            siMonths.Add(new SelectListItem() { Value = "4", Text = "April" });
            siMonths.Add(new SelectListItem() { Value = "5", Text = "May" });
            siMonths.Add(new SelectListItem() { Value = "6", Text = "June" });
            siMonths.Add(new SelectListItem() { Value = "7", Text = "July" });
            siMonths.Add(new SelectListItem() { Value = "8", Text = "August" });
            siMonths.Add(new SelectListItem() { Value = "9", Text = "September" });
            siMonths.Add(new SelectListItem() { Value = "10", Text = "October" });
            siMonths.Add(new SelectListItem() { Value = "11", Text = "November" });
            siMonths.Add(new SelectListItem() { Value = "12", Text = "December" });
            ViewBag.yearsSelect = new SelectList(siYears, "Value", "Text");
            ViewBag.monthsSelect = new SelectList(siMonths, "Value", "Text");

            if(TempData["Error"] != null)
            {
                ViewBag.Message = TempData["Error"];
            }
            return View();
        }

        // POST: MVCReport/DisplayReport
        [HttpPost]
        public ActionResult Report()
        {
            if(Request.Form["selectMonth"].Length < 1 || Request.Form["selectYear"].Length < 1 )
            {
                TempData["Error"] = "Please select a month and a year";
                return RedirectToAction("Index");
            }

            int monthnum = int.Parse(Request.Form["selectMonth"]);
            int yearid = int.Parse(Request.Form["selectYear"]);

            GoodSamaritanContext ctx = new GoodSamaritanContext();
            
            ViewBag.genderMale = (from c in ctx.Clients
                                  where c.Month == monthnum
                                  where c.FiscalYearId == yearid
                                  where c.Gender == "Male"
                                  select c).Count();

            ViewBag.genderFemale = (from c in ctx.Clients
                                    where c.Month == monthnum
                                    where c.FiscalYearId == yearid
                                    where c.Gender == "Female"
                                    select c).Count();

            ViewBag.genderTrans = (from c in ctx.Clients
                                   where c.Month == monthnum
                                   where c.FiscalYearId == yearid
                                   where c.Gender == "Trans"
                                   select c).Count();
            ViewBag.age12_19 = (from c in ctx.Clients
                                where c.Month == monthnum
                                where c.FiscalYearId == yearid
                                where c.Age.Range == "Youth >12<19"
                                select c).Count();

            ViewBag.age13 = (from c in ctx.Clients
                             where c.Month == monthnum
                             where c.FiscalYearId == yearid
                             where c.Age.Range == "Child <13"
                             select c).Count();

            ViewBag.age18_25 = (from c in ctx.Clients
                                where c.Month == monthnum
                                where c.FiscalYearId == yearid
                                where c.Age.Range == "Youth >18<25"
                                select c).Count();

            ViewBag.age24_65 = (int)(from c in ctx.Clients
                                     where c.Month == monthnum
                                     where c.FiscalYearId == yearid
                                     where c.Age.Range == "Adult >24<65"
                                     select c).Count();

            ViewBag.age64 = (from c in ctx.Clients
                             where c.Month == monthnum
                             where c.FiscalYearId == yearid
                             where c.Age.Range == "Senior >64"
                             select c).Count();

            ViewBag.statusReopened = (from c in ctx.Clients
                                      where c.Month == monthnum
                                      where c.FiscalYearId == yearid
                                      where c.StatusOfFile.Status == "Reopened"
                                      select c).Count();

            ViewBag.statusOpen = (from c in ctx.Clients
                                     where c.Month == monthnum
                                     where c.FiscalYearId == yearid
                                     where c.StatusOfFile.Status == "Open"
                                     select c).Count();

            ViewBag.statusClosed = (from c in ctx.Clients
                                       where c.Month == monthnum
                                       where c.FiscalYearId == yearid
                                       where c.StatusOfFile.Status == "Closed"
                                       select c).Count();
            
            ViewBag.programCrisis = (from c in ctx.Clients
                                        where c.Month == monthnum
                                        where c.FiscalYearId == yearid
                                        where c.Program.Type == "Crisis"
                                        select c).Count();

            ViewBag.programCourt = (from c in ctx.Clients
                                       where c.Month == monthnum
                                       where c.FiscalYearId == yearid
                                       where c.Program.Type == "Court"
                                       select c).Count();

            ViewBag.programSMART = (from c in ctx.Clients
                                       where c.Month == monthnum
                                       where c.FiscalYearId == yearid
                                       where c.Program.Type == "SMART"
                                       select c).Count();

            ViewBag.programDVU = (from c in ctx.Clients
                                     where c.Month == monthnum
                                     where c.FiscalYearId == yearid
                                     where c.Program.Type == "DVU"
                                     select c).Count();

            ViewBag.programMCFD = (from c in ctx.Clients
                                      where c.Month == monthnum
                                      where c.FiscalYearId == yearid
                                      where c.Program.Type == "MCFD"
                                      select c).Count();

            

            ViewBag.currentdate = DateTime.Now.ToString("MMMM dd/yyyy");

            ViewBag.year = (from c in ctx.FiscalYears
                            where c.FiscalYearId == yearid
                            select c.Years).First().ToString();

            String[] monthArray = {"January", "February", "March", "April", "May", "June", "July", "August",
                                  "September", "October", "November", "December"};

            ViewBag.month = monthArray[monthnum-1];
            return View();
        }
    }
}