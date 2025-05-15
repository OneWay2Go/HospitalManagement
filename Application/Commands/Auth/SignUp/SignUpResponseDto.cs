using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Application.Commands.Auth.SignUp
{
    public class SignUpResponseDto
    {
        public string Fullname { get; set; }

        public string PasswordHash { get; set; }

        public RoleType Role { get; set; }
    }
}
