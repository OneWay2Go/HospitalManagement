namespace HospitalManagement.Dtos
{
    public class ArrangeAppointmentsDto
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}
