using align.Models.Auth;
using align.Utils;
using Microsoft.AspNetCore.Identity;

namespace align.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<CreateAdminResponse>> CreateAdmin(string firstName, string lastName, string email, string phoneNumber, string password);
    }
}
