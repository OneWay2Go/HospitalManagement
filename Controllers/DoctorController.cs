using HospitalManagement.Application.Commands.CreateDoctor;
using HospitalManagement.Application.Queries.GetAllDoctors;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;
using HospitalManagement.Filters;
using HospitalManagement.Services.Doctors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = nameof(RoleType.Doctor))]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ISender _sender;

        public DoctorController(
            ISender sender,
            IDoctorService doctorService)
        {
            _sender = sender;
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctorAsync([FromBody] CreateDoctorDto dto)
        {
            await _sender.Send(new CreateDoctorCommand(dto));

            return Created();
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        [LogActionFilter]
        public IActionResult GetAllDoctors()
        {
            var doctors = _sender.Send(new GetAllDoctorsQuery());

            return Ok(doctors);
        }

        [HttpGet("api/Doctor/{id:int}")]
        [EnableRateLimiting("tokenBucket")]
        public IActionResult GetDoctor([FromRoute]int id)
        {
            var doctor = _doctorService.GetDoctorById(id);

            if (doctor == null)
            {
                return BadRequest($"There is no doctor with id - {id}");
            }

            return Ok(doctor);
        }
    }
}
