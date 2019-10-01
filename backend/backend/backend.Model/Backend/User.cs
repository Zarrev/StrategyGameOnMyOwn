using Microsoft.AspNetCore.Identity;

namespace backend.Model
{
    public class User: IdentityUser
    {
        public Country Country { get; set; }
    }
}
