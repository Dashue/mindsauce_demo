using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientAdministrationBoundedContext.Core
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllRegistered();
        Task<bool> Contains(Patient patient);
        Task Add(Patient patient);
    }
}