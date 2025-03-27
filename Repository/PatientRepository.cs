using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalManagement.Repository
{
    public class PatientRepository(HospitalContext context)
        : Repository<Patient>(context), IPatientRepository
    {
    }
}