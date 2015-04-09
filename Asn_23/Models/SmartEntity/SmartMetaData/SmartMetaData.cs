using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asn_23.Models
{
    [MetadataType(typeof(SmartMetaData))]
    public partial class Smart { }
    
    public class SmartMetaData
    {
        [HiddenInput(DisplayValue=false)]
        public virtual int ClientReferenceNumber { get; set; }

        [Display(Name = "Sex Work Exploitation Status")]
        public virtual int SexWorkExploitationId { get; set; }

        [Display(Name = "Multiple Perpetrators")]
        public virtual int MultiplePerpetratorsId { get; set; }

        [Display(Name = "Drug Facilitated Assault")]
        public virtual int DrugFacilitatedAssaultId { get; set; }

        [Display(Name = "City of Assault")]
        public virtual int CityOfAssaultId { get; set; }

        [Display(Name = "City of Residence")]
        public virtual int CityOfResidenceId { get; set; }

        [Display(Name = "Accompaniment Minutes")]
        public virtual int AccompanimnetMinutes { get; set; }

        [Display(Name = "Referring Hospital")]
        public virtual int ReferringHospitalId { get; set; }

        [Display(Name = "Hospital Attended")]
        public virtual int HospitalAttendedId { get; set; }

        [Display(Name = "Social Work Attendance")]
        public virtual int SocialWorkAttendanceId { get; set; }

        [Display(Name = "Police Attendance")]
        public virtual int PoliceAttendanceId { get; set; }

        [Display(Name = "Victim Service Attendance")]
        public virtual int VictimServiceAttendanceId { get; set; }

        [Display(Name = "Medical Only")]
        public virtual int MedicalOnlyId { get; set; }

        [Display(Name = "Evidence Stored")]
        public virtual int EvidenceStoredId { get; set; }

        [Display(Name = "HIV Meds")]
        public virtual int HIVMedsId { get; set; }

        [Display(Name = "Referred to CBVS")]
        public virtual int ReferredToCBVSId { get; set; }

        [Display(Name = "Police Reported")]
        public virtual int PoliceReportedId { get; set; }

        [Display(Name = "Third Party Report")]
        public virtual int ThirdPartyReportId { get; set; }

        [Display(Name = "Bad Date Report")]
        public virtual int BadDateReportId { get; set; }

        [Display(Name = "Referred To Nurse")]
        public virtual bool ReferredToNursePractitioner { get; set; }

        
        public virtual Clients Clients { get; set; }
        public virtual SexWorkExploitation SexWorkExploitation { get; set; }
        public virtual MultiplePerpetrators MultiplePerpetrators { get; set; }
        public virtual DrugFacilitatedAssault DrugFacilitatedAssault { get; set; }
        public virtual CityOfAssault CityOfAssault { get; set; }
        public virtual CityOfResidence CityOfResidence { get; set; }
        public virtual ReferringHospital ReferringHospital { get; set; }
        public virtual HospitalAttended HospitalAttended { get; set; }
        public virtual SocialWorkAttendance SocialWorkAttendance { get; set; }
        public virtual PoliceAttendance PoliceAttendance { get; set; }
        public virtual VictimServicesAttendance VictimServicesAttendance { get; set; }
        public virtual MedicalOnly MedicalOnly { get; set; }
        public virtual EvidenceStored EvidenceStored { get; set; }
        public virtual HIVMeds HIVMeds { get; set; }
        public virtual ReferredToCBVS ReferredToCBVS { get; set; }
        public virtual PoliceReported PoliceReported { get; set; }
        public virtual ThirdPartyReport ThirdPartyReport { get; set; }
        public virtual BadDateReport BadDateReport { get; set; }
    }
}