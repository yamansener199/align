using align.Data;
using align.Data.Entities;
using align.Models.Auth;
using align.Models.User;
using align.Utils;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace align.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<Data.Entities.User> _userManager;
        private readonly SignInManager<Data.Entities.User> _signInManager;


        private readonly AppDbContext _context;
        public UserService(AppDbContext context, UserManager<Data.Entities.User> userManager, SignInManager<Data.Entities.User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ServiceResponse<UserModel>> AddUser([FromBody] AddUserRequestModel request)
        {
            var entity = new Data.Entities.User()
            {
                UserName = GenerateRandomUserName(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserRole = request.IsRegionManager ? UserRole.RegionManager : UserRole.Admin
            };

            var result = await _userManager.CreateAsync(entity, request.Password);

            if (result.Succeeded)
            {
                if (!request.IsRegionManager)
                {
                    await _userManager.AddToRoleAsync(entity, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(entity, "Manager");
                }

                // await _signInManager.SignInAsync(entity, isPersistent: false);

                return new ServiceResponse<UserModel>()
                {
                    Data = new UserModel
                    {
                        CreatedAt = DateTime.UtcNow,
                        Email = request.Email,
                        FirstName = request.FirstName,
                        Id = entity.Id,
                        IsSuperAdmin = !request.IsRegionManager,
                        LastName= request.LastName,
                        PhoneNumber = request.PhoneNumber
                    },
                    ErrorMessage = null,
                    StatusCode = 200
                };
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ServiceResponse<UserModel>
                {
                    Data = null,
                    ErrorMessage = errors,
                    StatusCode = 400
                };
            }

        }

        public async Task<ServiceResponse<List<UserModel>>> GetUsers()
        {
            var users = await _context.Users.Where(x => !x.IsDeleted).ToListAsync();

            var response = users.Select(x => new UserModel 
            {
                FirstName = x.FirstName,
                CreatedAt = x.CreatedAt,
                Email = x.Email,
                Id = x.Id,
                IsSuperAdmin = x.UserRole == Data.Entities.UserRole.Admin ? true : false,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            }
            ).OrderByDescending(x=>x.CreatedAt).ToList();

            return new ServiceResponse<List<UserModel>>()
            {
                Data = response,
                ErrorMessage = null,
                StatusCode = 200
            };
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

        public async Task<ServiceResponse<UserModel>> UpdateUser(UpdateUserRequestModel updatedUser)
        {
            var user = await _context.Users.Where(x => x.Id == updatedUser.Id && !x.IsDeleted).SingleOrDefaultAsync();

            if (user is null) 
            {
                return new ServiceResponse<UserModel>()
                {
                    Data = null,
                    ErrorMessage = "Kullanıcı bulunamadı.",
                    StatusCode = 404
                };
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.UserRole = updatedUser.UserRole == "Admin" ? UserRole.Admin : UserRole.RegionManager;

            await _context.SaveChangesAsync();

            await AddIdentityRole(user.Id, user.UserRole);

            return new ServiceResponse<UserModel>()
            {
                Data = new UserModel
                {
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    IsSuperAdmin = user.UserRole == UserRole.Admin,
                    PhoneNumber = user.PhoneNumber
                },
                ErrorMessage = null,
                StatusCode = 200
            };

        }

        private async Task AddIdentityRole(string id, UserRole userRole)
        {
            var userFromDb = await _userManager.FindByIdAsync(id);

            var existingRoles = await _userManager.GetRolesAsync(userFromDb);
            await _userManager.RemoveFromRolesAsync(userFromDb, existingRoles);

            if(userRole == UserRole.Admin)
            {
                await _userManager.AddToRoleAsync(userFromDb, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(userFromDb, "Manager");
            }

            await _userManager.UpdateAsync(userFromDb);
        }
    }
}
