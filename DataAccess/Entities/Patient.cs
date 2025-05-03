namespace HospitalManagement.DataAccess.Entities;

public class Patient
{
    public Patient()
    {
        Appointments = new List<Appointment>();
    }

    public int PatientId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

    public int? PatientBlankId { get; set; }

    public PatientBlank? PatientBlank { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}
