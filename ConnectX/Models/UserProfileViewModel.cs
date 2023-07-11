using System.ComponentModel.DataAnnotations;

namespace ConnectX.Models
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ProfilePic { get; set; }
        public int UserId { get; internal set; }
    }
}
