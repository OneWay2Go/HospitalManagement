using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;

namespace HospitalManagement.Repository;

public class DoctorRepository(HospitalContext context)
    : Repository<Doctor>(context), IDoctorRepository
{

}
