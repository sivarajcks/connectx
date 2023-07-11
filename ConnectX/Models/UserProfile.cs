using System.ComponentModel.DataAnnotations;

namespace ConnectX.Models
{
    public class UserProfile
    {
        [Key]
        //public int UserId { get; set; }        
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Location { get; set; }
        public string ProfilePic { get; set; }
    }
}
