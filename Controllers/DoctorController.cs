using HospitalManagement.Dtos;
using HospitalManagement.Filters;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctorAsync([FromBody] CreateDoctorDto doctorDto)
        {
            await _doctorService.CreateDoctorAsync(doctorDto);

            return Created();
        }

        [HttpGet]
        [LogActionFilter]
        public IActionResult GetAllDoctors()
        {
            var doctors = _doctorService.GetAllDoctors();

            return Ok(doctors);
        }
    }
}
