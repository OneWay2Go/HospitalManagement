namespace HospitalManagement.DataAccess.Entities
{
    public class CancelAppointmentRequest
    {
        public int AppointmentId { get; set; }

        public string Reason { get; set; }
    }
}
