using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Repository
{
    public class UserRepository(HospitalContext context) : Repository<User>(context), IUserRepository
    {
        private readonly HospitalContext _context = context;

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
