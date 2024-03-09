namespace align.Models.Auth
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
