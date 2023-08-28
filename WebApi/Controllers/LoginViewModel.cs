using Microsoft.AspNetCore.Components.Web;

namespace WebApi.Controllers
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}