using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }

    public class UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<UpdateDepartmentCommand>
    {
        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id);

            departmentRepository.Update(department);
            await departmentRepository.SaveChangesAsync();
        }
    }
}
