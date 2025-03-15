using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Dtos;

public class CreateDoctorDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int SpecialityId { get; set; }

    public Doctor ToEntity()
    {
        return new()
        {
            FirstName = FirstName,
            LastName = LastName,
            SpecialityId = SpecialityId,
            IsActive = true
        };
    }
}
