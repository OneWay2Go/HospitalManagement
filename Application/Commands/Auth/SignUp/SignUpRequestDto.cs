using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Application.Commands.Auth.SignUp
{
    public class SignUpRequestDto
    {
        public string Fullname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public RoleType Role { get; set; }
    }
}
