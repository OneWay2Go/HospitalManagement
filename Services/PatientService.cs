using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services
{
    public interface IPatientService
    {
        IQueryable<PatientDto> GetPatients();
    }
    public class PatientService : IPatientService
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;

        public PatientService(
            HospitalContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<PatientDto> GetPatients()
        {
            return _context.Patients
                .ProjectTo<PatientDto>(_mapper.ConfigurationProvider);
        }
    }
}
