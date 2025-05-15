using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services.Auth;
using HospitalManagement.Services.Hasher;
using MediatR;

namespace HospitalManagement.Application.Commands.Auth.SignIn
{
    public class SignInCommand(SignInRequestDto request) : IRequest<SignInResponseDto>
    {
        public SignInRequestDto Request { get; } = request;
    }

    public class SignInCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IAuthService authService) : IRequestHandler<SignInCommand, SignInResponseDto>
    {
        public async Task<SignInResponseDto> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var user = await userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var isValidPassword = passwordHasher.VerifyHash(request.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = authService.GetToken(user.Fullname);

            return new SignInResponseDto()
            {
                AccessToken = token,
            };
        }
    }
}
