using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Repository.Interfaces
{
    public class AppointmentRepository(HospitalContext context) : 
        Repository<Appointment>(context), IAppointmentRepository 
    {
    }
}
