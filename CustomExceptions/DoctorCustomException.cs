namespace HospitalManagement.CustomExceptions
{
    public class DoctorCustomException : Exception
    {
        public DateTime Time { get; }

        public DoctorCustomException() { }

        public DoctorCustomException(string message) : base(message) { }

        public DoctorCustomException(string message, DateTime time) : base(message)
        {
            Time = time;
        }
    }
}
