using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services
{
    public interface IPatientService
    {
        IQueryable<PatientDto> GetPatients();
        Task<PatientDto> GetPatientByIdAsync(int id);
    }
    public class PatientService : IPatientService
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;

        public PatientService(
            HospitalContext context,
            IMapper mapper,
            IPatientRepository patientRepository)
        {
            _context = context;
            _mapper = mapper;
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            var patientDto = _mapper.Map<PatientDto>(patient);

            return patientDto;
        }

        public IQueryable<PatientDto> GetPatients()
        {
            return _context.Patients
                .ProjectTo<PatientDto>(_mapper.ConfigurationProvider);
        }
    }
}
