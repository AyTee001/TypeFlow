using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TypeFlow.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public override string? UserName { get; set; }

        [Required]
        public override string? Email { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
