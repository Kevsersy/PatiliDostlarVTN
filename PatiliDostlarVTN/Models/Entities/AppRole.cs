using Microsoft.AspNetCore.Identity;

namespace PatiliDostlarVTN.Models.Entities
{
    public class AppRole:IdentityRole
    {

        public DateTime CreatedAt { get; set; }

    }
}
