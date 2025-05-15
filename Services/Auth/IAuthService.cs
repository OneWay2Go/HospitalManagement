namespace HospitalManagement.Services.Auth
{
    public interface IAuthService
    {
        string GetToken(string username);
    }
}
