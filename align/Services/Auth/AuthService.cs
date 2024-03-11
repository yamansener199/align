using align.Data;
using align.Models.Auth;
using align.Utils;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace align.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<Data.Entities.User> _signInManager;
        private readonly UserManager<Data.Entities.User> _userManager;

        public AuthService(AppDbContext context, SignInManager<Data.Entities.User> signInManager, UserManager<Data.Entities.User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<CreateAdminResponse>> CreateAdmin(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string password)
        {
            var entity = new Data.Entities.User
            {
                Email = email,
                PhoneNumber = phoneNumber,
                UserName = GenerateRandomUserName(),
                FirstName = firstName,
                LastName = lastName,
            };

            var result = await _userManager.CreateAsync(entity, password);

            if (result.Succeeded)
            {
                // Assign the "Admin" role to the user
                await _userManager.AddToRoleAsync(entity, "Admin");

                await _signInManager.SignInAsync(entity, isPersistent: false);

                return new ServiceResponse<CreateAdminResponse>()
                {
                    Data = new CreateAdminResponse
                    {
                        UserId = entity.Id,
                        UserRole = "Admin",
                    },
                    ErrorMessage = null,
                    StatusCode = 200
                };
            }
            else
            {
                // Handle errors during user creation
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ServiceResponse<CreateAdminResponse>
                {
                    Data = null,
                    ErrorMessage = errors,
                    StatusCode = 400
                };
            }

        }

        private static string GenerateRandomUserName(int? length = 8)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return GenerateRandomString(allowedChars, length ?? 8);
        }

        private static string GenerateRandomString(string allowedChars, int length)
        {
            if (length < 1)
            {
                throw new ArgumentException("Length must be greater than or equal to 1", nameof(length));
            }

            char[] randomChars = new char[length];

            using (RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[1];

                for (int i = 0; i < length; i++)
                {
                    cryptoProvider.GetBytes(randomNumber);
                    int rand = Convert.ToInt32(randomNumber[0]);

                    randomChars[i] = allowedChars[rand % allowedChars.Length];
                }
            }

            return new string(randomChars);
        }
    }
}
