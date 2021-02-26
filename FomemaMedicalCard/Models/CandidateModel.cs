using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FomemaMedicalCard.Models
{
    public class CandidateModel
    {
        public int CandidateId { get; set; }
        public Guid CandidateGuid { get; set; }
        public string CandidateCardNumber { get; set; }
        public string CandidateCardNumberEncrypted { get; set; }


        public string Name { get; set; }
        public string DateofBirth { get; set; }
        public int? Age { get; set; }
        public string NewPassportNo { get; set; }
        public string OldPassportNo { get; set; }
        /// <summary>
        /// Upload time base 64 of image is come here
        /// </summary>
        public string PictureURL { get; set; }
        public HttpPostedFileBase PictureFromComputer { get; set; }
        public bool IsImageRemove { get; set; }

        public string CountryName { get; set; }
        public bool IsDeleted { get; set; }
        public string RedirectedPath { get; set; }
        public int? FomemaTestYear { get; set; }
        public string FomemaTestResult { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int SrNo { get; set; }
        public string Covid19TestDate1 { get; set; }
        public string Covid19TestDate2 { get; set; }
        public string Covid19TestDate3 { get; set; }
        public string Covid19TestDate4 { get; set; }
        public string VaccineDose1Date { get; set; }
        public string VaccineDose2Date { get; set; }
        public string ClinicName { get; set; }
        public string ClinicLocation { get; set; }

        public string DTClinicName { get; set; }
        public string DTClinicLocation { get; set; }

        public DateTime DTCovid19TestDate1 { get; set; }
        public DateTime DTCovid19TestDate2 { get; set; }
        public DateTime DTCovid19TestDate3 { get; set; }
        public DateTime DTCovid19TestDate4 { get; set; }
        public DateTime DTVaccineDose1Date { get; set; }
        public DateTime DTVaccineDose2Date { get; set; }
    }
}