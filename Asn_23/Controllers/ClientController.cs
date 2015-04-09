using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asn_23.Models;

namespace Asn_23.Controllers
{
    public class ClientController : Controller
    {
        private GoodSamaritanContext db = new GoodSamaritanContext();
        private SmartController sc = new SmartController();

        // GET: Client
        [Authorize(Roles = "Administrator,Worker,Reporter")]
        public async Task<ActionResult> Index()
        //public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.AbuserRelationship).Include(c => c.Age).Include(c => c.AssignedWorker).Include(c => c.Crisis).Include(c => c.DuplicateFile).Include(c => c.Ethnicity).Include(c => c.FamilyViolenceFile).Include(c => c.FiscalYear).Include(c => c.Incident).Include(c => c.Program).Include(c => c.ReferralContact).Include(c => c.ReferralSource).Include(c => c.RepeatClient).Include(c => c.RiskLevel).Include(c => c.RiskStatus).Include(c => c.Service).Include(c => c.StatusOfFile).Include(c => c.VictimOfIncident);
            return View(await clients.ToListAsync());
            //return View("Manage");
        }

        // GET: Client/Details/5
        [Authorize(Roles = "Administrator,Worker,Reporter")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = await db.Clients.FindAsync(id);
            if (clients == null)
            {
                return HttpNotFound();
            }

            ViewBag.smartObject = new Smart();

            if (clients.ClientReferenceNumber == 3)
            {
                var smartId = (from s in db.Smarts
                               where s.ClientReferenceNumber == clients.ClientReferenceNumber
                               select (s.SmartId)).SingleOrDefault();

                Smart smart = await db.Smarts.FindAsync(smartId);

                ViewBag.smartObject = smart;
            }

            ViewBag.ProgramId = clients.ProgramId;

            return View(clients);
        }

        // GET: Client/Create
        [Authorize(Roles = "Administrator,Worker")]
        public ActionResult Create()
        {
            ViewBag.AbuserRelationshipId = new SelectList(db.AbuserRelationships, "AbuserRelationshipId", "Relationship");
            ViewBag.AgeId = new SelectList(db.Ages, "AgeId", "Range");
            ViewBag.AssignedWorkerId = new SelectList(db.AssignedWorkers, "AssignedWorkerId", "Name");
            ViewBag.CrisisId = new SelectList(db.Crises, "CrisisId", "Type");
            ViewBag.DuplicateFileId = new SelectList(db.DuplicateFiles, "DuplicateFileId", "YesOrNull");
            ViewBag.EthnicityId = new SelectList(db.Ethnicities, "EthnicityId", "Description");
            ViewBag.FamilyViolenceFileId = new SelectList(db.FamilyViolenceFiles, "FamilyViolenceFileId", "FileExists");
            ViewBag.FiscalYearId = new SelectList(db.FiscalYears, "FiscalYearId", "Years");
            ViewBag.IncidentId = new SelectList(db.Incidents, "IncidentId", "Type");
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Type");
            ViewBag.ReferralContactId = new SelectList(db.ReferralContacts, "ReferralContactId", "Contact");
            ViewBag.ReferralSourceId = new SelectList(db.ReferralSources, "ReferralSourceId", "Source");
            ViewBag.RepeatClientId = new SelectList(db.RepeatClients, "RepeatClientId", "YesOrNull");
            ViewBag.RiskLevelId = new SelectList(db.RiskLevels, "RiskLevelId", "Level");
            ViewBag.RiskStatusId = new SelectList(db.RiskStatus, "RiskStatusId", "Status");
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "Type");
            ViewBag.StatusOfFileId = new SelectList(db.StatusOfFiles, "StatusOfFileId", "Status");
            ViewBag.VictimOfIncidentId = new SelectList(db.VictimOfIncidents, "VictimOfIncidentId", "PrimaryOrSecondary");
            
            // smart entity select lists
            //Dictionary<String,SelectList> selectLists = sc.GetSelectLists();

            ViewBag.BadDateReportId = new SelectList(db.BadDateReports, "BadDateReportId", "YesNoNA");
            ViewBag.CityOfAssaultId = new SelectList(db.CityOfAssaults, "CityOfAssaultId", "City");
            ViewBag.CityOfResidenceId = new SelectList(db.CityOfResidences, "CityOfResidenceId", "City");
            ViewBag.ClientReferenceNumber = new SelectList(db.Clients, "ClientReferenceNumber", "Surname");
            ViewBag.DrugFacilitatedAssaultId = new SelectList(db.DrugFacilitatedAssaults, "DrugFacilitatedAssaultId", "YesNoNA");
            ViewBag.EvidenceStoredId = new SelectList(db.EvidenceStoreds, "EvidenceStoredId", "YesNoNA");
            ViewBag.HIVMedsId = new SelectList(db.HIVMeds, "HIVMedsId", "YesNoNA");
            ViewBag.HospitalAttendedId = new SelectList(db.HospitalAttendeds, "HospitalAttendedId", "HospitalName");
            ViewBag.MedicalOnlyId = new SelectList(db.MedicalOnlies, "MedicalOnlyId", "YesNoNA");
            ViewBag.MultiplePerpetratorsId = new SelectList(db.MultiplePerpetrators, "MultiplePerpetratorsId", "YesNoNA");
            ViewBag.PoliceAttendanceId = new SelectList(db.PoliceAttendances, "PoliceAttendanceId", "YesNoNA");
            ViewBag.PoliceReportedId = new SelectList(db.PoliceReporteds, "PoliceReportedId", "YesNoNA");
            ViewBag.ReferredToCBVSId = new SelectList(db.ReferredToCBVS, "ReferredToCBVSId", "YesNoPVBSOnlyNA");
            ViewBag.ReferringHospitalId = new SelectList(db.ReferringHospitals, "ReferringHospitalId", "HospitalName");
            ViewBag.SexWorkExploitationId = new SelectList(db.SexWorkExploitations, "SexWorkExploitationId", "YesNoNA");
            ViewBag.SocialWorkAttendanceId = new SelectList(db.SocialWorkAttendances, "SocialWorkAttendanceId", "YesNoNA");
            ViewBag.ThirdPartyReportId = new SelectList(db.ThirdPartyReports, "ThirdPartyReportId", "YesNoNA");
            ViewBag.VictimServicesAttendanceId = new SelectList(db.VictimServicesAttendances, "VictimServicesAttendanceId", "YesNoNA");

            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<ActionResult> Create([Bind(Include = "ClientReferenceNumber,FiscalYearId,Month,Day,Surname,FirstName,PoliceFileNumber,CourtFileNumber,SWCFileNumber,RiskLevelId,CrisisId,ServiceId,ProgramId,RiskAssessmentAssignedTo,RiskStatusId,AssignedWorkerId,ReferralSourceId,ReferralContactId,IncidentId,AbuserSurnameFirstName,AbuserRelationshipId,VictimOfIncidentId,FamilyViolenceFileId,Gender,EthnicityId,AgeId,RepeatClientId,DuplicateFileId,NumberOfChildren0to6,NumberOfChildren7to12,NumberOfChildren13to18,StatusOfFileId,DateLastTransferred,DateClosed,DateReOpened")] Clients clients,
            [Bind(Include = "SmartId,ClientReferenceNumber,SexWorkExploitationId,MultiplePerpetratorsId,DrugFacilitatedAssaultId,CityOfAssaultId,CityOfResidenceId,AccompanimnetMinutes,ReferringHospitalId,HospitalAttendedId,SocialWorkAttendanceId,PoliceAttendanceId,VictimServiceAttendanceId,MedicalOnlyId,EvidenceStoredId,HIVMedsId,ReferredToCBVSId,PoliceReportedId,ThirdPartyReportId,BadDateReportId,ReferredToNursePractitioner")] Smart smart)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clients);
                await db.SaveChangesAsync();

                smart.ClientReferenceNumber = clients.ClientReferenceNumber;

                // if they selected the SMART program,
                // send smart data to the smart controller
                if (clients.ProgramId == 3)
                {
                    await sc.Create(smart, true);
                }

                return RedirectToAction("Index");
            }

            ViewBag.AbuserRelationshipId = new SelectList(db.AbuserRelationships, "AbuserRelationshipId", "Relationship", clients.AbuserRelationshipId);
            ViewBag.AgeId = new SelectList(db.Ages, "AgeId", "Range", clients.AgeId);
            ViewBag.AssignedWorkerId = new SelectList(db.AssignedWorkers, "AssignedWorkerId", "Name", clients.AssignedWorkerId);
            ViewBag.CrisisId = new SelectList(db.Crises, "CrisisId", "Type", clients.CrisisId);
            ViewBag.DuplicateFileId = new SelectList(db.DuplicateFiles, "DuplicateFileId", "YesOrNull", clients.DuplicateFileId);
            ViewBag.EthnicityId = new SelectList(db.Ethnicities, "EthnicityId", "Description", clients.EthnicityId);
            ViewBag.FamilyViolenceFileId = new SelectList(db.FamilyViolenceFiles, "FamilyViolenceFileId", "FileExists", clients.FamilyViolenceFileId);
            ViewBag.FiscalYearId = new SelectList(db.FiscalYears, "FiscalYearId", "Years", clients.FiscalYearId);
            ViewBag.IncidentId = new SelectList(db.Incidents, "IncidentId", "Type", clients.IncidentId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Type", clients.ProgramId);
            ViewBag.ReferralContactId = new SelectList(db.ReferralContacts, "ReferralContactId", "Contact", clients.ReferralContactId);
            ViewBag.ReferralSourceId = new SelectList(db.ReferralSources, "ReferralSourceId", "Source", clients.ReferralSourceId);
            ViewBag.RepeatClientId = new SelectList(db.RepeatClients, "RepeatClientId", "YesOrNull", clients.RepeatClientId);
            ViewBag.RiskLevelId = new SelectList(db.RiskLevels, "RiskLevelId", "Level", clients.RiskLevelId);
            ViewBag.RiskStatusId = new SelectList(db.RiskStatus, "RiskStatusId", "Status", clients.RiskStatusId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "Type", clients.ServiceId);
            ViewBag.StatusOfFileId = new SelectList(db.StatusOfFiles, "StatusOfFileId", "Status", clients.StatusOfFileId);
            ViewBag.VictimOfIncidentId = new SelectList(db.VictimOfIncidents, "VictimOfIncidentId", "PrimaryOrSecondary", clients.VictimOfIncidentId);
            return View(clients);
        }

        // GET: Client/Edit/5
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Clients clients = await db.Clients.FindAsync(id);
            if (clients == null)
            {
                return HttpNotFound();
            }

            var query = (from s in db.Smarts
                        where s.ClientReferenceNumber == id
                         select (s.SmartId)).SingleOrDefault();

            Smart smart = await db.Smarts.FindAsync(query);
            
            if(smart == null)
            {
                smart = new Smart();
            }
            
            ViewBag.AbuserRelationshipId = new SelectList(db.AbuserRelationships, "AbuserRelationshipId", "Relationship", clients.AbuserRelationshipId);
            ViewBag.AgeId = new SelectList(db.Ages, "AgeId", "Range", clients.AgeId);
            ViewBag.AssignedWorkerId = new SelectList(db.AssignedWorkers, "AssignedWorkerId", "Name", clients.AssignedWorkerId);
            ViewBag.CrisisId = new SelectList(db.Crises, "CrisisId", "Type", clients.CrisisId);
            ViewBag.DuplicateFileId = new SelectList(db.DuplicateFiles, "DuplicateFileId", "YesOrNull", clients.DuplicateFileId);
            ViewBag.EthnicityId = new SelectList(db.Ethnicities, "EthnicityId", "Description", clients.EthnicityId);
            ViewBag.FamilyViolenceFileId = new SelectList(db.FamilyViolenceFiles, "FamilyViolenceFileId", "FileExists", clients.FamilyViolenceFileId);
            ViewBag.FiscalYearId = new SelectList(db.FiscalYears, "FiscalYearId", "Years", clients.FiscalYearId);
            ViewBag.IncidentId = new SelectList(db.Incidents, "IncidentId", "Type", clients.IncidentId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Type", clients.ProgramId);
            ViewBag.ReferralContactId = new SelectList(db.ReferralContacts, "ReferralContactId", "Contact", clients.ReferralContactId);
            ViewBag.ReferralSourceId = new SelectList(db.ReferralSources, "ReferralSourceId", "Source", clients.ReferralSourceId);
            ViewBag.RepeatClientId = new SelectList(db.RepeatClients, "RepeatClientId", "YesOrNull", clients.RepeatClientId);
            ViewBag.RiskLevelId = new SelectList(db.RiskLevels, "RiskLevelId", "Level", clients.RiskLevelId);
            ViewBag.RiskStatusId = new SelectList(db.RiskStatus, "RiskStatusId", "Status", clients.RiskStatusId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "Type", clients.ServiceId);
            ViewBag.StatusOfFileId = new SelectList(db.StatusOfFiles, "StatusOfFileId", "Status", clients.StatusOfFileId);
            ViewBag.VictimOfIncidentId = new SelectList(db.VictimOfIncidents, "VictimOfIncidentId", "PrimaryOrSecondary", clients.VictimOfIncidentId);

            // smart entity select lists
            //Dictionary<String, SelectList> selectLists = sc.GetSelectLists();

            ViewBag.BadDateReportId = new SelectList(db.BadDateReports, "BadDateReportId", "YesNoNA", smart.BadDateReportId);
            ViewBag.CityOfAssaultId = new SelectList(db.CityOfAssaults, "CityOfAssaultId", "City", smart.CityOfAssaultId);
            ViewBag.CityOfResidenceId = new SelectList(db.CityOfResidences, "CityOfResidenceId", "City", smart.CityOfResidenceId);
            ViewBag.ClientReferenceNumber = new SelectList(db.Clients, "ClientReferenceNumber", "Surname", smart.ClientReferenceNumber);
            ViewBag.DrugFacilitatedAssaultId = new SelectList(db.DrugFacilitatedAssaults, "DrugFacilitatedAssaultId", "YesNoNA", smart.DrugFacilitatedAssaultId);
            ViewBag.EvidenceStoredId = new SelectList(db.EvidenceStoreds, "EvidenceStoredId", "YesNoNA", smart.EvidenceStoredId);
            ViewBag.HIVMedsId = new SelectList(db.HIVMeds, "HIVMedsId", "YesNoNA", smart.HIVMedsId);
            ViewBag.HospitalAttendedId = new SelectList(db.HospitalAttendeds, "HospitalAttendedId", "HospitalName", smart.HospitalAttendedId);
            ViewBag.MedicalOnlyId = new SelectList(db.MedicalOnlies, "MedicalOnlyId", "YesNoNA", smart.MedicalOnlyId);
            ViewBag.MultiplePerpetratorsId = new SelectList(db.MultiplePerpetrators, "MultiplePerpetratorsId", "YesNoNA", smart.MultiplePerpetratorsId);
            ViewBag.PoliceAttendanceId = new SelectList(db.PoliceAttendances, "PoliceAttendanceId", "YesNoNA", smart.PoliceAttendanceId);
            ViewBag.PoliceReportedId = new SelectList(db.PoliceReporteds, "PoliceReportedId", "YesNoNA", smart.PoliceReportedId);
            ViewBag.ReferredToCBVSId = new SelectList(db.ReferredToCBVS, "ReferredToCBVSId", "YesNoPVBSOnlyNA", smart.ReferredToCBVSId);
            ViewBag.ReferringHospitalId = new SelectList(db.ReferringHospitals, "ReferringHospitalId", "HospitalName", smart.ReferringHospitalId);
            ViewBag.SexWorkExploitationId = new SelectList(db.SexWorkExploitations, "SexWorkExploitationId", "YesNoNA", smart.SexWorkExploitationId);
            ViewBag.SocialWorkAttendanceId = new SelectList(db.SocialWorkAttendances, "SocialWorkAttendanceId", "YesNoNA", smart.SocialWorkAttendanceId);
            ViewBag.ThirdPartyReportId = new SelectList(db.ThirdPartyReports, "ThirdPartyReportId", "YesNoNA", smart.ThirdPartyReportId);
            ViewBag.VictimServicesAttendanceId = new SelectList(db.VictimServicesAttendances, "VictimServicesAttendanceId", "YesNoNA", smart.VictimServiceAttendanceId);

            return View(clients);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<ActionResult> Edit([Bind(Include = "ClientReferenceNumber,FiscalYearId,Month,Day,Surname,FirstName,PoliceFileNumber,CourtFileNumber,SWCFileNumber,RiskLevelId,CrisisId,ServiceId,ProgramId,RiskAssessmentAssignedTo,RiskStatusId,AssignedWorkerId,ReferralSourceId,ReferralContactId,IncidentId,AbuserSurnameFirstName,AbuserRelationshipId,VictimOfIncidentId,FamilyViolenceFileId,Gender,EthnicityId,AgeId,RepeatClientId,DuplicateFileId,NumberOfChildren0to6,NumberOfChildren7to12,NumberOfChildren13to18,StatusOfFileId,DateLastTransferred,DateClosed,DateReOpened")] Clients clients,
            [Bind(Include = "SmartId,ClientReferenceNumber,SexWorkExploitationId,MultiplePerpetratorsId,DrugFacilitatedAssaultId,CityOfAssaultId,CityOfResidenceId,AccompanimnetMinutes,ReferringHospitalId,HospitalAttendedId,SocialWorkAttendanceId,PoliceAttendanceId,VictimServiceAttendanceId,MedicalOnlyId,EvidenceStoredId,HIVMedsId,ReferredToCBVSId,PoliceReportedId,ThirdPartyReportId,BadDateReportId,ReferredToNursePractitioner")] Smart smart)
        {
            if (ModelState.IsValid)
            {
                // get the programid set in the db before this edit
                var clientsQuery =  (from c in db.Clients
                                    where c.ClientReferenceNumber == clients.ClientReferenceNumber
                                    select (c.ProgramId)).SingleOrDefault();
                
                db.Entry(clients).State = EntityState.Modified;
                await db.SaveChangesAsync();

                // if they selected the SMART program,
                // send smart data to the smart controller
                if (clients.ProgramId == 3)
                {
                    var smartEntity =   (from s in db.Smarts
                                        where s.ClientReferenceNumber == clients.ClientReferenceNumber
                                        select (s.SmartId)).FirstOrDefault();

                    if (smartEntity == 0)
                    {
                        smart.ClientReferenceNumber = clients.ClientReferenceNumber;
                        await sc.Create(smart, true);
                    }
                    else
                    {
                        await sc.Edit(smart, true);
                    }
                }
                // if it used to be the SMART program but has changed,
                // delete SMART program data for this client
                else if(clientsQuery == 3)
                {
                    var smartId =   (from s in db.Smarts
                                    where s.ClientReferenceNumber == clients.ClientReferenceNumber
                                    select (s.SmartId)).SingleOrDefault();

                    await sc.DeleteConfirmed(smartId);
                }

                return RedirectToAction("Index");
            }

            ViewBag.AbuserRelationshipId = new SelectList(db.AbuserRelationships, "AbuserRelationshipId", "Relationship", clients.AbuserRelationshipId);
            ViewBag.AgeId = new SelectList(db.Ages, "AgeId", "Range", clients.AgeId);
            ViewBag.AssignedWorkerId = new SelectList(db.AssignedWorkers, "AssignedWorkerId", "Name", clients.AssignedWorkerId);
            ViewBag.CrisisId = new SelectList(db.Crises, "CrisisId", "Type", clients.CrisisId);
            ViewBag.DuplicateFileId = new SelectList(db.DuplicateFiles, "DuplicateFileId", "YesOrNull", clients.DuplicateFileId);
            ViewBag.EthnicityId = new SelectList(db.Ethnicities, "EthnicityId", "Description", clients.EthnicityId);
            ViewBag.FamilyViolenceFileId = new SelectList(db.FamilyViolenceFiles, "FamilyViolenceFileId", "FileExists", clients.FamilyViolenceFileId);
            ViewBag.FiscalYearId = new SelectList(db.FiscalYears, "FiscalYearId", "Years", clients.FiscalYearId);
            ViewBag.IncidentId = new SelectList(db.Incidents, "IncidentId", "Type", clients.IncidentId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Type", clients.ProgramId);
            ViewBag.ReferralContactId = new SelectList(db.ReferralContacts, "ReferralContactId", "Contact", clients.ReferralContactId);
            ViewBag.ReferralSourceId = new SelectList(db.ReferralSources, "ReferralSourceId", "Source", clients.ReferralSourceId);
            ViewBag.RepeatClientId = new SelectList(db.RepeatClients, "RepeatClientId", "YesOrNull", clients.RepeatClientId);
            ViewBag.RiskLevelId = new SelectList(db.RiskLevels, "RiskLevelId", "Level", clients.RiskLevelId);
            ViewBag.RiskStatusId = new SelectList(db.RiskStatus, "RiskStatusId", "Status", clients.RiskStatusId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "Type", clients.ServiceId);
            ViewBag.StatusOfFileId = new SelectList(db.StatusOfFiles, "StatusOfFileId", "Status", clients.StatusOfFileId);
            ViewBag.VictimOfIncidentId = new SelectList(db.VictimOfIncidents, "VictimOfIncidentId", "PrimaryOrSecondary", clients.VictimOfIncidentId);
            return View(clients);
        }

        // GET: Client/Delete/5
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = await db.Clients.FindAsync(id);
            if (clients == null)
            {
                return HttpNotFound();
            }

            ViewBag.smartObject = new Smart();

            if(clients.ClientReferenceNumber == 3)
            {
                var smartId = (from s in db.Smarts
                               where s.ClientReferenceNumber == clients.ClientReferenceNumber
                               select (s.SmartId)).SingleOrDefault();

                Smart smart = await db.Smarts.FindAsync(smartId);

                ViewBag.smartObject = smart;
            }

            ViewBag.ProgramId = clients.ProgramId;
            

            return View(clients);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clients clients = await db.Clients.FindAsync(id);
            
            // remove clients and smart entity, if exists
            db.Clients.Remove(clients);
            
            if(clients.ProgramId == 3)
            {
                var smartId = (from s in db.Smarts
                               where s.ClientReferenceNumber == clients.ClientReferenceNumber
                               select (s.SmartId)).SingleOrDefault();
                
                Smart smart = await db.Smarts.FindAsync(smartId);
                db.Smarts.Remove(smart);
            }

            // save changes
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
