using HospitalManagement.Application.Commands.CreateDepartment;
using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQuery(int id) : IRequest<DepartmentDto>
    {
        public int Id {  get; set; } = id;
    }

    public class GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id);

            var departmentDto = department.ToDto();

            return departmentDto;
        }
    }
}
