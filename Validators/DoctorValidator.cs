using FluentValidation;
using HospitalManagement.Dtos;

namespace HospitalManagement.Validators
{
    public class DoctorValidator : AbstractValidator<CreateDoctorDto>
    {
        public DoctorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname have to be filled!")
                .Length(3, 50).WithMessage("Firstname have to be from 3 to 50 characters!");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname have to be filled!")
                .Length(3, 50).WithMessage("Lastname have to be from 3 to 50 characters!");

            RuleFor(x => x.SpecialityId)
                .NotEmpty().WithMessage("SpecialityId have to be filled!")
                .GreaterThan(0).WithMessage("SpecialityId have to be greater than 0!");
        }
    }
}
