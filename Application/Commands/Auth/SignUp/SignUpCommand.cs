using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services.Hasher;
using MediatR;

namespace HospitalManagement.Application.Commands.Auth.SignUp
{
    public class SignUpCommand(SignUpRequestDto request) : IRequest<SignUpResponseDto>
    {
        public SignUpRequestDto Request { get; } = request;
    }

    public class SignUpCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<SignUpCommand, SignUpResponseDto>
    {
        public async Task<SignUpResponseDto> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (!Enum.IsDefined(typeof(RoleType), request.Role))
            {
                throw new InvalidDataException("Invalid role type is given");
            }

            var user = new User()
            {
                Email = request.Email,
                Fullname = request.Fullname,
                PasswordHash = passwordHasher.HashPassword(request.Password),
                Role = request.Role,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            return new SignUpResponseDto()
            {
                Fullname = request.Fullname,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };
        }
    }
}
