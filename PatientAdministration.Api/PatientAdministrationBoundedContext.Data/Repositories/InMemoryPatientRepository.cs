using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientAdministrationBoundedContext.Core;

namespace PatientAdministrationBoundedContext.Data
{
    public class InMemoryPatientRepository : IPatientRepository
    {
        private List<Patient> registeredPatients = new List<Patient>();

        public InMemoryPatientRepository()
        {
            // Pre-register my existing patients
            registeredPatients.Add(Patient.New("Alpha", "Jones", new DateTime(1982, 02, 15), "Remember to follow up"));
            registeredPatients.Add(Patient.New("Beta", "Smith", new DateTime(1946, 07, 02)));
        }

        public Task<bool> Contains(Patient patient)
        {
            bool contains= registeredPatients.Any(
                x => x.Firstname.ToLower() == patient.Firstname.ToLower() &&
                     x.Surname.ToLower() == patient.Surname.ToLower() &&
                     x.Birthdate == patient.Birthdate);

            return Task.FromResult(contains);
        }

        public Task<List<Patient>> GetAllRegistered()
        {
            return Task.FromResult(registeredPatients);
        }

        public async Task Add(Patient patient)
        {
            if (await Contains(patient))
            {
                return;
            }
            else
            {
                registeredPatients.Add(patient);
            }
        }
    }
}