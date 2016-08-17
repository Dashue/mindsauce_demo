using FluentValidation;
using PatientAdministration.Api.Requests;

namespace PatientAdministration.Api.Validation
{
    public class AddPatientValidator : AbstractValidator<AddPatient>
    {
        public AddPatientValidator()
        {
            RuleFor(request => request.Firstname).Length(2, 50).WithMessage("You must specify a Firstname between 2 and 50.");
            RuleFor(request => request.Surname).Length(3, 50).WithMessage("You must specify a long enough surname.");
            RuleFor(request => request.Birthdate).NotEmpty().WithMessage("You must specify date of birth.");
        }
    }
}