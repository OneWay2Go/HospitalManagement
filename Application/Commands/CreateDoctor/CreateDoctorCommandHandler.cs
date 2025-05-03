using HospitalManagement.Repository.Interfaces;
using MediatR;

namespace HospitalManagement.Application.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler(IDoctorRepository doctorRepository) : IRequestHandler<CreateDoctorCommand>
    {
        public async Task Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = request.Dto.ToEntity();

            await doctorRepository.AddAsync(doctor);
            await doctorRepository.SaveChangesAsync();
        }
    }
}
