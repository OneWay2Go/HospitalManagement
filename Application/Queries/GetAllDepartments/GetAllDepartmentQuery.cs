using HospitalManagement.Application.Commands.CreateDepartment;
using HospitalManagement.Migrations;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Queries.GetAllDepartments
{
    public class GetAllDepartmentQuery : IRequest<List<DepartmentDto>>
    {
    }

    public class GetAllDepartmentQueryHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetAllDepartmentQuery, List<DepartmentDto>>
    {
        public Task<List<DepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = departmentRepository.GetAll().ToList();

            var departmentsDto = departments.Select(d => d.ToDto());

            return (Task<List<DepartmentDto>>)departmentsDto;
        }
    }
}
