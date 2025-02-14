namespace Factory_Management_System_WebAPI.Models
{
    public class AuthModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class LoginResponse
    {
        public int UserID { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class UserModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? AdminID { get; set; }
    }
}
