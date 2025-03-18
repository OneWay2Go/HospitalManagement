using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace HospitalManagement.Services
{
    public interface IAppointmentService
    {
        bool CancelAppointment(DateTime appointmentDate, DateTime requestDate);
        Appointment? GetById(int id);
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentSettings _settings;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IOptions<AppointmentSettings> settings, IAppointmentRepository appointmentRepository)
        {
            _settings = settings.Value;
            _appointmentRepository = appointmentRepository;
        }

        public bool CancelAppointment(DateTime appointmentDate, DateTime requestDate)
        {
            DateTime deadline = appointmentDate.AddHours(-_settings.CancellationDeadlineHours);

            return requestDate <= deadline;
        }

        public Appointment? GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }
    }
}
