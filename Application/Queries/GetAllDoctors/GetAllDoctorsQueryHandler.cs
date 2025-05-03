using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalManagement.Dtos;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Application.Queries.GetAllDoctors
{
    public class GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository, IMapper mapper) : IRequestHandler<GetAllDoctorsQuery, List<DoctorDto>>
    {
        public async Task<List<DoctorDto>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = doctorRepository.GetAll().AsNoTracking().ProjectTo<DoctorDto>(mapper.ConfigurationProvider).ToList();

            return doctors;
        }
    }
}
