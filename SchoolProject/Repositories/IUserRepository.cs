using SchoolProject.Models;

namespace SchoolProject.Repositories
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<User> Register(RegistrationRequest registrationRequest);
    }
}
