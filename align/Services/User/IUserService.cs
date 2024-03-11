using align.Models.User;
using align.Utils;

namespace align.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<UserModel>> AddUser(AddUserRequestModel request);
        Task<ServiceResponse<List<UserModel>>> GetUsers();
        Task<ServiceResponse<UserModel>> UpdateUser(UpdateUserRequestModel updatedUser);
    }
}
