using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options;
using Serilog.Context;
using System.Runtime.CompilerServices;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DoctorsSettings _workTime;
        private readonly IPatientRepository _context;
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(
            IOptions<DoctorsSettings> workTime, 
            IPatientRepository patientRepo, 
            IAppointmentService appointmentService, 
            IAppointmentRepository appointmentRepository,
            ILogger<PatientsController> logger) 
        {
            _workTime = workTime.Value;
            _context = patientRepo;
            _appointmentService = appointmentService;
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        [HttpGet("get-all-patients")]
        public IActionResult GetAll()
        {
            using (LogContext.PushProperty("PatientId", 1))
            {
                _logger.LogInformation("Hello!");

                _logger.LogError("Hello!");
            }

            var patients = _context.GetAll();

            return Ok(patients);
        }

        [HttpPost("arrange-appointments")]
        public async Task<IActionResult> ArrangeAppointments([FromBody] ArrangeAppointmentsDto arrangeAppointmentsDto)
        {
            var time = TimeOnly.FromDateTime(arrangeAppointmentsDto.AppointmentDate);
            
            if (time.IsBetween(_workTime.workTime.Start, _workTime.workTime.End))
            {
                await _appointmentRepository.AddAsync(new Appointment
                {
                    AppointmentDate = arrangeAppointmentsDto.AppointmentDate,
                    IsActive = true,
                    DoctorId = arrangeAppointmentsDto.DoctorId,
                    PatientId = arrangeAppointmentsDto.PatientId
                });
                
                await _appointmentRepository.SaveChangesAsync();

                return Ok("Arrangement created!");
            }

            return BadRequest("No doctor for that time! (Work time of doctors: 8:00 -> 17:00)");
        }

        [HttpPost("cancel-appointment")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentRequest request)
        {
            DateTime requestedAt = DateTime.Now;

            var appointment = _appointmentService.GetById(request.AppointmentId);

            if (appointment == null)
            {
                return NotFound($"There is no appointment with id - {request.AppointmentId}");
            }

            bool canCancel = _appointmentService.CancelAppointment(appointment.AppointmentDate, DateTime.Now);

            if (!canCancel)
            {
                return BadRequest("Can not cancel appointment due to the deadline!");
            }

            appointment.IsActive = false;

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return Ok("Appointment canceled successfully!");
        }
    }
}
