using align.Models.User;
using align.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace align.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("/User/GetSessionUserInfo")]
        public async Task<ActionResult<UserModel>> GetSessionUserInfo()
        {
            string userId = HttpContext.User.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;

            if(userId is null)
            {
                return BadRequest();
            }

            var result = await _userService.GetUserById(userId);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status403Forbidden, "Oopss!");
        }

    }
}
