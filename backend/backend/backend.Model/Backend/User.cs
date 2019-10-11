using backend.Model.Backend;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace backend.Model
{
    public class User: IdentityUser
    {
        public virtual Country Country { get; set; }
        public virtual List<Battle> Battles { get; set; }
    }
}
