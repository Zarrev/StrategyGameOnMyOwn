using System.ComponentModel.DataAnnotations;

namespace backend.Model.Frontend.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }

    }
}
