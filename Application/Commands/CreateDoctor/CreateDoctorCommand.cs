using HospitalManagement.Dtos;
using MediatR;

namespace HospitalManagement.Application.Commands.CreateDoctor
{
    public class CreateDoctorCommand(CreateDoctorDto dto) : IRequest
    {
        public CreateDoctorDto Dto { get; set; } = dto;
    }
}
