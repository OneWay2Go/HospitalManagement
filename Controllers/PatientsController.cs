﻿using AutoMapper;
using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog.Context;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DoctorsSettings _workTime;
        private readonly HospitalContext _context;
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientsController> _logger;
        private readonly AppointmentSettings _settings;
        private readonly IPatientService _patientService;

        public PatientsController(
            IOptions<AppointmentSettings> settings,
            IOptions<DoctorsSettings> workTime, 
            HospitalContext context, 
            IAppointmentService appointmentService, 
            IAppointmentRepository appointmentRepository,
            IMapper mapper,
            IPatientService patientService,
            ILogger<PatientsController> logger)
        {
            _workTime = workTime.Value;
            _context = context;
            _appointmentService = appointmentService;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _logger = logger;
            _settings = settings.Value;
            _patientService = patientService;
        }

        [HttpGet("get-all-patients")]
        public IActionResult GetAll()
        {
            var patientsDto = _patientService.GetPatients().ToList();

            //IList<PatientDto> patientDtos = new List<PatientDto>();

            //foreach(var patient in patients)
            //{
            //    patientDtos.Add(_mapper.Map<PatientDto>(patient));
            //}

            return Ok(patientsDto);
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

            DateTime deadline = appointment.AppointmentDate.AddHours(-_settings.CancellationDeadlineHours);

            bool canCancel = DateTime.Now <= deadline ? true : false;
                
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
