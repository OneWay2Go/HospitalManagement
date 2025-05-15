using HospitalManagement.DataAccess.Entities;

namespace HospitalManagement.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
