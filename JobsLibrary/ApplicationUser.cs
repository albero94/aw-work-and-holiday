using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsLibrary
{
    public class ApplicationUser : IdentityUser
    {
        [Required]

        [Column("name")]
        public string Name { get; set; }
        [Column("is_company")]
        public bool isCompany { get; set; }
    }
}
