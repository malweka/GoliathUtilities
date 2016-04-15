namespace Goliath.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ExternalLoginModel
    {
        public string ReturnUrl { get; set; }
        public string Provider { get; set; }
    }
}