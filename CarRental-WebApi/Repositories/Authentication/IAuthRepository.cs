using CarRental_WebApi.Dtos.UserAuthentication;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Repositories.Authentication
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDto userRequest);
        Task<bool> UserExists(string email);
    }
}