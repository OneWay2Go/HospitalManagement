using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using MediatR;
using System.Runtime.CompilerServices;

namespace HospitalManagement.Application.Commands.CreateDepartment
{
    public class DepartmentDto
    {
        public string DepartmentName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public Department ToEntity()
        {
            return new()
            {
                DepartmentName = DepartmentName,
                Location = Location,
                Description = Description,
            };
        }
    }

    public class CreateDepartmentCommand(DepartmentDto dto) : IRequest
    {
        public DepartmentDto Dto { get; set; } = dto;
    }

    public class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<CreateDepartmentCommand>
    {
        public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = request.Dto.ToEntity();

            await departmentRepository.AddAsync(department);
            await departmentRepository.SaveChangesAsync();
        }
    }
}
