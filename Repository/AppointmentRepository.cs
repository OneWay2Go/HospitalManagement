using HospitalManagement.CustomExceptions;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Repository
{
    public class AppointmentRepository(HospitalContext context) :
        Repository<Appointment>(context), IAppointmentRepository
    {
        private readonly HospitalContext _context = context;

        public async Task ScheduleAppointment(AppointmentScheduleDto scheduleDto)
        {
            var doctor = await _context.Doctors.Include(d => d.Appointments).FirstOrDefaultAsync(d => d.DoctorId == scheduleDto.DoctorId);

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == scheduleDto.PatientId);

            if (doctor.Appointments.Any(d => d.EndTime > scheduleDto.StartTime))
            {
                throw new DoctorCustomException($"Problems with doctor(id: {doctor.DoctorId}).");
            }


        }
    }
}
