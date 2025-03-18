namespace HospitalManagement.DataAccess.Entities
{
    public class AppointmentSettings
    {
        public int CancellationDeadlineHours { get; set; }

        public int NotificationReminderHours { get; set; }
    }
}
