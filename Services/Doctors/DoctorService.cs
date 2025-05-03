using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Doctors
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(CreateDoctorDto doctorDto);

        IList<DoctorDto> GetAllDoctors();

        DoctorDto GetDoctorById(int id);
    }

    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task CreateDoctorAsync(CreateDoctorDto doctorDto)
        {
            var doctor = doctorDto.ToEntity();

            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();
        }

        public IList<DoctorDto> GetAllDoctors()
        {
            return _doctorRepository.GetAll().AsNoTracking().ProjectTo<DoctorDto>(_mapper.ConfigurationProvider).ToList();
        }

        public DoctorDto GetDoctorById(int id)
        {
            var doctor = _doctorRepository.GetById(id);

            var doctorDto = _mapper.Map<DoctorDto>(doctor);

            return doctorDto;
        }
    }
}
