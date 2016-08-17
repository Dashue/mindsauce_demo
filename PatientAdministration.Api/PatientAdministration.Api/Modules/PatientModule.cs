using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using PatientAdministration.Api.Requests;
using PatientAdministration.Api.Responses;
using PatientAdministrationBoundedContext.Core;

namespace PatientAdministration.Api.Modules
{
    public class PatientModule : NancyModule
    {
        public PatientModule(IPatientRepository patientRepository): base("api/patients")
        {
            Get("/registered", async x =>
            {
                List<Patient> registeredPatients = await patientRepository.GetAllRegistered();
                
                var response = new RegisteredPatientsResponse
                {
                    Result = registeredPatients
                };

                return Response.AsJson(response);
            });

            Post("/add", async x =>
            {
                var request = this.BindAndValidate<AddPatient>();
                if (false == ModelValidationResult.IsValid)
                {
                    IEnumerable<dynamic> formattedErrors = ModelValidationResult.FormattedErrors;
                    return Response.AsJson(formattedErrors, HttpStatusCode.UnprocessableEntity);
                }

                var patient = Patient.New(request.Firstname, request.Surname, request.Birthdate, request.Note);

                if (await patientRepository.Contains(patient))
                {
                    return HttpStatusCode.Conflict;
                }
                else
                {
                    await patientRepository.Add(patient);
                    return HttpStatusCode.OK;
                }
            });
        }
    }
}