using HospitalManagement.Application.Commands.CreateDepartment;
using HospitalManagement.Application.Commands.DeleteDepartment;
using HospitalManagement.Application.Queries.GetAllDepartments;
using HospitalManagement.Application.Queries.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ISender _sender;

        public DepartmentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto dto)
        {
            await _sender.Send(new CreateDepartmentCommand(dto));

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departmentsDto = await _sender.Send(new GetAllDepartmentQuery());

            return Ok(departmentsDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentById([FromRoute]int id)
        {
            var departmentDto = await _sender.Send(new GetDepartmentByIdQuery(id));

            return Ok(departmentDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment([FromRoute]int id)
        {
            await _sender.Send(new DeleteDepartmentCommand(id));

            return Ok();
        }
    }
}
