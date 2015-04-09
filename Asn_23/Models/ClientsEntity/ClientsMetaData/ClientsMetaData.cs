using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Asn_23.Models
{
    [MetadataType(typeof(ClientsMetaData))]
    public partial class Clients { }
    
    public class ClientsMetaData
    {
        public int ClientReferenceNumber { get; set; }

        [Display(Name = "Fiscal Year")]
        public virtual int FiscalYearId { get; set; }

        public virtual int Month { get; set; }

        public virtual int Day { get; set; }

        [MaxLength(50)]
        public virtual string Surname { get; set; }

        [MaxLength(50)]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [MaxLength(8)]
        [Display(Name = "Police File Number")]
        public virtual string PoliceFileNumber { get; set; }

        [Display(Name = "Court File Number")]
        public virtual int CourtFileNumber { get; set; }

        [Display(Name = "SWC File Number")]
        public virtual int SWCFileNumber { get; set; }

        [Display(Name = "Risk Level")]
        public virtual int RiskLevelId { get; set; }

        [Display(Name = "Crisis")]
        public virtual int CrisisId { get; set; }

        [Display(Name = "Service")]
        public virtual int ServiceId { get; set; }

        [Display(Name = "Program")]
        public virtual int ProgramId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Assessment Assigned To")]
        public virtual string RiskAssessmentAssignedTo { get; set; }

        [Display(Name = "Risk Status")]
        public virtual int RiskStatusId { get; set; }

        [Display(Name = "Assigned Worker")]
        public virtual int AssignedWorkerId { get; set; }

        [Display(Name = "Referral Source")]
        public virtual int ReferralSourceId { get; set; }

        [Display(Name = "Referral Contact")]
        public virtual int ReferralContactId { get; set; }

        [Display(Name = "Incident")]
        public virtual int IncidentId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Abuser Name (Surname, First Name)")]
        public virtual string AbuserSurnameFirstName { get; set; }

        [Display(Name = "Abuser Relationship")]
        public virtual int AbuserRelationshipId { get; set; }

        [Display(Name = "Victim Of Incident")]
        public virtual int VictimOfIncidentId { get; set; }

        [Display(Name = "Family Violence File")]
        public virtual int FamilyViolenceFileId { get; set; }

        [MaxLength(5)]
        public virtual string Gender { get; set; }

        [Display(Name = "Ethnicity")]
        public virtual int EthnicityId { get; set; }

        [Display(Name = "Age")]
        public virtual int AgeId { get; set; }

        [Display(Name = "Repeat Client")]
        public virtual int RepeatClientId { get; set; }

        [Display(Name = "Duplicate File")]
        public virtual int DuplicateFileId { get; set; }

        [Display(Name = "Number Of Childern Age 0 - 6")]
        public virtual int NumberOfChildren0to6 { get; set; }

        [Display(Name = "Number Of Childern Age 7 - 12")]
        public virtual int NumberOfChildren7to12 { get; set; }

        [Display(Name = "Number Of Childern Age 13 - 18")]
        public virtual int NumberOfChildren13to18 { get; set; }

        [Display(Name = "Status Of File")]
        public virtual int StatusOfFileId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Last Transferred")]
        public virtual DateTime DateLastTransferred { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Closed")]
        public virtual DateTime DateClosed { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Re-opened")]
        public virtual DateTime DateReOpened { get; set; }

        public virtual IEnumerable<Smart> Smart { get; set; }

        [Display(Name = "Fiscal Year")]
        public virtual FiscalYear FiscalYear { get; set; }

        [Display(Name = "Risk Level")]
        public virtual RiskLevel RiskLevel { get; set; }

        public virtual Crisis Crisis { get; set; }

        public virtual Service Service { get; set; }

        public virtual Program Program { get; set; }

        [Display(Name = "Risk Status")]
        public virtual RiskStatus RiskStatus { get; set; }

        [Display(Name="Assigned Worker")]
        public virtual AssignedWorker AssignedWorker { get; set; }

        [Display(Name = "Referral Source")]
        public virtual ReferralSource ReferralSource { get; set; }

        [Display(Name = "Referral Contact")]
        public virtual ReferralContact ReferralContact { get; set; }

        public virtual Incident Incident { get; set; }

        [Display(Name = "Abuser Relationship")]
        public virtual AbuserRelationship AbuserRelationship { get; set; }

        [Display(Name = "Victim of Incident")]
        public virtual VictimOfIncident VictimOfIncident { get; set; }

        [Display(Name = "Family Violence File")]
        public virtual FamilyViolenceFile FamilyViolenceFile { get; set; }

        public virtual Ethnicity Ethnicity { get; set; }

        public virtual Age Age { get; set; }

        [Display(Name = "Repeat Client")]
        public virtual RepeatClient RepeatClient { get; set; }

        [Display(Name = "Duplicate File")]
        public virtual DuplicateFile DuplicateFile { get; set; }

        [Display(Name = "Status Of File")]
        public virtual StatusOfFile StatusOfFile { get; set; }
    }
}