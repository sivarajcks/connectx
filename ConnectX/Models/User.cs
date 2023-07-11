using System.ComponentModel.DataAnnotations;

namespace ConnectX.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
