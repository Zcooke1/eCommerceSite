using System.ComponentModel.DataAnnotations;
using System.Composition.Convention;

namespace eCommerceSite.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Username { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name ="Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof (Password))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
