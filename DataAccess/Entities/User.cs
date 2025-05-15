namespace HospitalManagement.DataAccess.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Fullname { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public RoleType Role { get; set; }
    }
}
