using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }

    public class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<DeleteDepartmentCommand>
    {
        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id);

            departmentRepository.Delete(department);
            await departmentRepository.SaveChangesAsync();
        }
    }
}
