using HospitalManagement.Application.Commands.CreateDepartment;
using HospitalManagement.Migrations;
using HospitalManagement.Repository.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Application.Queries.GetAllDepartments
{
    public class GetAllDepartmentQuery : IRequest<List<DepartmentDto>>
    {
    }

    public class GetAllDepartmentQueryHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetAllDepartmentQuery, List<DepartmentDto>>
    {
        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetAll().ToListAsync();

            var departmentsDto = departments.Select(d => d.ToDto()).ToList();

            return departmentsDto;
        }
    }
}
