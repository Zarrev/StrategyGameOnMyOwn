using System.ComponentModel.DataAnnotations;

namespace backend.Model.Frontend.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string RepeatedPassword { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string CountryName { get; set; }
    }
}
