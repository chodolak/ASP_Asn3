using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Asn_23.Models;
using Newtonsoft.Json;

namespace Asn_23.Controllers
{
    [Authorize(Roles = "Administrator,Reporter,Worker")]
    public class ReportController : ApiController
    {
        public class YearObj
        {
            public int id;
            public string year;
        }

        public class ReportObj
        {
            public int genderFemale;
            public int genderMale;
            public int genderTrans;

            public int age24_65;
            public int age18_25;
            public int age12_19;
            public int age13;
            public int age64;

            public int statusOpen;
            public int statusClosed;
            public int statusReopened;

            public int programCrisis;
            public int programCourt;
            public int programSMART;
            public int programDVU;
            public int programMCFD;
        }


        // GET api/report/getyear
        public IEnumerable<YearObj> GetYear()
        {
            GoodSamaritanContext ctx = new GoodSamaritanContext();
            List<YearObj> yearList = new List<YearObj>();
            var qry = (from c in ctx.FiscalYears
                      select new { c.FiscalYearId, c.Years });
            foreach (var item in qry)
            {
                yearList.Add(new YearObj { id = item.FiscalYearId, year = item.Years });
            }

            return yearList;
        }

        [HttpGet]
        public ReportObj GetReport(int monthnum, int yearid)
        {
            GoodSamaritanContext ctx = new GoodSamaritanContext();
            ReportObj reportData = new ReportObj();

            reportData.genderMale = (from c in ctx.Clients
                                     where c.Month == monthnum
                                     where c.FiscalYearId == yearid
                                     where c.Gender == "Male"
                                     select c).Count();
            reportData.genderFemale = (from c in ctx.Clients
                                       where c.Month == monthnum
                                       where c.FiscalYearId == yearid
                                       where c.Gender == "Female"
                                       select c).Count();
            reportData.genderTrans = (from c in ctx.Clients
                                      where c.Month == monthnum
                                      where c.FiscalYearId == yearid
                                      where c.Gender == "Trans"
                                      select c).Count();

            reportData.age24_65 = (int)(from c in ctx.Clients
                                        where c.Month == monthnum
                                        where c.FiscalYearId == yearid
                                        where c.Age.Range == "Adult >24<65"
                                        select c).Count();

            reportData.age18_25 = (from c in ctx.Clients
                                   where c.Month == monthnum
                                   where c.FiscalYearId == yearid
                                   where c.Age.Range == "Youth >18<25"
                                   select c).Count();

            reportData.age12_19 = (from c in ctx.Clients
                                   where c.Month == monthnum
                                   where c.FiscalYearId == yearid
                                   where c.Age.Range == "Youth >12<19"
                                   select c).Count();

            reportData.age13 = (from c in ctx.Clients
                                where c.Month == monthnum
                                where c.FiscalYearId == yearid
                                where c.Age.Range == "Child <13"
                                select c).Count();

            reportData.age64 = (from c in ctx.Clients
                                where c.Month == monthnum
                                where c.FiscalYearId == yearid
                                where c.Age.Range == "Senior >64"
                                select c).Count();

            reportData.statusOpen = (from c in ctx.Clients
                                    where c.Month == monthnum
                                    where c.FiscalYearId == yearid
                                    where c.StatusOfFile.Status == "Open"
                                    select c).Count();
            reportData.statusClosed = (from c in ctx.Clients
                                     where c.Month == monthnum
                                     where c.FiscalYearId == yearid
                                     where c.StatusOfFile.Status == "Closed"
                                     select c).Count();
            reportData.statusReopened = (from c in ctx.Clients
                                     where c.Month == monthnum
                                     where c.FiscalYearId == yearid
                                     where c.StatusOfFile.Status == "Reopened" 
                                     select c).Count();

            reportData.programCrisis = (from c in ctx.Clients
                                         where c.Month == monthnum
                                         where c.FiscalYearId == yearid
                                         where c.Program.Type == "Crisis"
                                         select c).Count();
            reportData.programCourt = (from c in ctx.Clients
                                        where c.Month == monthnum
                                        where c.FiscalYearId == yearid
                                        where c.Program.Type == "Court"
                                           select c).Count();

            reportData.programSMART = (from c in ctx.Clients
                                        where c.Month == monthnum
                                        where c.FiscalYearId == yearid
                                        where c.Program.Type == "SMART"
                                           select c).Count();

            reportData.programDVU = (from c in ctx.Clients
                                        where c.Month == monthnum
                                        where c.FiscalYearId == yearid
                                        where c.Program.Type == "DVU"
                                         select c).Count();

            reportData.programMCFD = (from c in ctx.Clients
                                        where c.Month == monthnum
                                        where c.FiscalYearId == yearid
                                        where c.Program.Type == "MCFD"
                                          select c).Count();

            return reportData;
        }
    }
}
