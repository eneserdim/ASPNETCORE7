using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace Core_Web_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Ogrencino { get; set; }
        public string? Adres { get; set; }
        public string? Fakulte { get; set; }
        public string? Bolum { get; set; }
    }
}
