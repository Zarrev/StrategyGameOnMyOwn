using backend.Model.Backend;
using Microsoft.AspNetCore.Identity;

namespace backend.Model
{
    public class User: IdentityUser
    {
        public virtual Country Country { get; set; }
    }
}
