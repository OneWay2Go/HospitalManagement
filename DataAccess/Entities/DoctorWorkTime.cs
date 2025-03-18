namespace HospitalManagement.DataAccess.Entities
{
    public class DoctorsSettings
    {
        public DoctorWorkTime workTime { get; set; }
    }

    public class DoctorWorkTime
    {
        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }
    }
}
