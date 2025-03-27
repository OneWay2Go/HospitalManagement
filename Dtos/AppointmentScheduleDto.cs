using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Dtos
{
    public class AppointmentScheduleDto
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public int SpecialityId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
