﻿namespace align.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
