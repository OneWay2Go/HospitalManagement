using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalManagement.Repository
{
    public class PatientRepository(HospitalContext context, IMemoryCache memoryCashe)
        : Repository<Patient>(context), IPatientRepository
    {
        public async Task<Patient> GetByIdAsync(int id)
        {
            // check cashe
            if(memoryCashe.TryGetValue(id, out Patient patient))
            {
                // if found in cashe, return it
                return patient;
            }

            // if not found in cashe, get from database
            patient = await context.Patients
                .FindAsync(id);

            // set to cashe
            memoryCashe.Set(id, patient, TimeSpan.FromSeconds(10));

            // return the patient
            return patient;
        }
    }
}