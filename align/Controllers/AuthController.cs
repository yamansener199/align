using align.Data;
using align.Data.Entities;
using align.Models.Auth;
using align.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace align.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext context, IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("Auth/AdminCreate")]
        public async Task<IActionResult> AdminCreatePostAsync([FromBody] AdminCreateRequestModel request)
        {
            var result = await _authService.CreateAdmin(request.FirstName,
                                                        request.LastName,
                                                        request.Email,
                                                        request.PhoneNumber,
                                                        request.Password);

            if (result.isSuccess)
            {
                return Ok(result.Data);
            }

            return StatusCode(403, result.ErrorMessage);
        }

        [HttpPost("Auth/Login")]
        public async Task<IActionResult> LoginPostAsync([FromBody] LoginRequestModel request)
        {
            var isValidUser = await ValidateUserCredentialsAndSignIn(request.Email, request.Password, request.IsSuperAdmin);

            if (isValidUser)
            {
                return Ok(new LoginViewModel
                {
                    Role = request.IsSuperAdmin ? UserRole.Admin.ToString() : UserRole.RegionManager.ToString(),
                    Email = request.Email
                });
            }
            else
            {
                return StatusCode(403, "Hatali şifre veya email.");
            }
        }

        private async Task<bool> ValidateUserCredentialsAndSignIn(string email, string password, bool isSuperAdmin)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null && IsUserRoleMatch(user, isSuperAdmin))
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        private bool IsUserRoleMatch(User user, bool isSuperAdmin)
        {
            if(isSuperAdmin && user.UserRole is not UserRole.Admin)
            {
                return false;
            }

            return true;
        }
    }
}
