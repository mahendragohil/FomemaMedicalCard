using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FomemaMedicalCard.Models
{
    public class ClinicRegistrationModel
    {
        public string ClinicName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorsICNumber { get; set; }
        public string Password { get; set; }
    }
}