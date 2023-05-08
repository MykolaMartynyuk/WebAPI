using System.Globalization;

namespace WebAPI.Models
{
    public class RegisterModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
