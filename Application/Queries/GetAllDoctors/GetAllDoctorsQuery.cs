using HospitalManagement.Dtos;
using MediatR;

namespace HospitalManagement.Application.Queries.GetAllDoctors
{
    public class GetAllDoctorsQuery : IRequest<List<DoctorDto>>
    {
    }
}
