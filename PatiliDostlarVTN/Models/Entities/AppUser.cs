using Microsoft.AspNetCore.Identity;

namespace PatiliDostlarVTN.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime BoD { get; set; }
        public DateTime CreatedAt { get; set; }
     
     
    }
}
