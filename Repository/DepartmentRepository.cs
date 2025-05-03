using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;

namespace HospitalManagement.Repository
{
    public class DepartmentRepository(HospitalContext context) : 
        Repository<Department>(context), IDepartmentRepository
    {
    }
}
