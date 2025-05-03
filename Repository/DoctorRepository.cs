using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalManagement.Repository;

public class DoctorRepository(HospitalContext context, IMemoryCache memoryCache)
    : Repository<Doctor>(context), IDoctorRepository
{
    public async Task<Doctor> GetByIdAsync(int id)
    {
        if (memoryCache.TryGetValue(id, out Doctor doctor))
        {
            return doctor;
        }

        doctor = await context.Doctors.FindAsync(id);
        
        if (doctor != null)
        {
            memoryCache.Set(id, doctor);
        }

        return doctor;
    }
}
