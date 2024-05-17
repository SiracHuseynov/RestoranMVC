using System.ComponentModel.DataAnnotations;

namespace Restorann.ViewModels
{
    public class MemberRegisterVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
        public bool IsPersistent { get; set; }


    }
}
