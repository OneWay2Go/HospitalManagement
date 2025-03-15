using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Doctors
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(CreateDoctorDto doctorDto);

        IList<DoctorDto> GetAllDoctors();
    }

    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task CreateDoctorAsync(CreateDoctorDto doctorDto)
        {
            var doctor = doctorDto.ToEntity();

            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();
        }

        public IList<DoctorDto> GetAllDoctors()
        {
            return _doctorRepository.GetAll().AsNoTracking().Select(r => new DoctorDto
            {
                DoctorId = r.DoctorId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                SpecialityId = r.SpecialityId
            }).ToList();
        }
    }
}
