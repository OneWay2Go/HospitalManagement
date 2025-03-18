﻿using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;

namespace HospitalManagement.Repository
{
    public class PatientRepository(HospitalContext context) 
        : Repository<Patient>(context), IPatientRepository
    {
    }
}
