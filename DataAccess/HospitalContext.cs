using HospitalManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.DataAccess;

public class HospitalContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Speciality> Specialities { get; set; }

    public DbSet<PatientBlank> PatientBlanks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.AppointmentId);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments);
        });

        modelBuilder.Entity<Doctor>(builder =>
        {
            builder.HasKey(d => d.DoctorId);

            builder.HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor);

            builder.HasOne(d => d.Speciality)
                .WithMany();
        });
        
        modelBuilder.Entity<Patient>(builder =>
        {
            builder.HasKey(p => p.PatientId);

            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient);

            builder.HasOne(p => p.PatientBlank)
                .WithOne(pb => pb.Patient)
                .HasForeignKey<Patient>(p => p.PatientBlankId);
        });
        
        modelBuilder.Entity<PatientBlank>(builder =>
        {
            builder.HasKey(r => r.PatientBlankId);

            builder.HasOne(r => r.Patient)
                .WithOne(r => r.PatientBlank)
                .HasForeignKey<PatientBlank>(r => r.PatientId);
        });
        
        modelBuilder.Entity<Speciality>(builder =>
        {
            builder.HasKey(s => s.SpecialityId);
        });
    }

    public HospitalContext(DbContextOptions options) : base(options) { }

    public HospitalContext()
    {
    }
}
