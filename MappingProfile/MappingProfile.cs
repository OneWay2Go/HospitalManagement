using AutoMapper;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;

namespace HospitalManagement.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString()))
                .ForMember(dest => dest.BlankIdentifier, opt => opt.MapFrom(src => src.PatientBlank.BlankIdentifier));
        }
    }
}
