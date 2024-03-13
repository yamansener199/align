using align.Data.Entities;
using align.Models.User;

namespace align.Mappers
{
    public static class UserMapper
    {
        public static UserModel ToModel(this User user)
        {
            return new UserModel
            {
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                IsSuperAdmin = user.UserRole == UserRole.Admin,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static List<UserModel> ToModelList(this List<User> users) 
        {
            return users.Select(x => ToModel(x)).ToList();
        }

    }
}
