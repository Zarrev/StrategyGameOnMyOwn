using Microsoft.AspNetCore.Identity;

namespace backend.Model
{
    public class User: IdentityUser
    {
        public Country Country { get; set; }

        // maybe I have to save the country key as well here, beacuse If I try to get the country with a rest end point then I cannot

    }
}
