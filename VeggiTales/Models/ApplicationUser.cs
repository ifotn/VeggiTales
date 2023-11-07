using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VeggiTales.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(2)]
        public string? Province { get; set; }
    }
}
