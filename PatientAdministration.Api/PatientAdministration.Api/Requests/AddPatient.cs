using System;

namespace PatientAdministration.Api.Requests
{
    public class AddPatient
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Note { get; set; }
    }
}