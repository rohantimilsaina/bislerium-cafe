using bislerium_cafe_pos.Utils;

namespace bislerium_cafe_pos.Models
{
    // Represents a user in the cafe management system.
    public class User
    {
        // Gets or sets the username of the user.
        //[Required]
        //[StringLength(8, ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        // Gets or sets the role of the user (e.g., Administrator, Staff).
        //[Required]
        //[StringLength(8, ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        // Gets or sets the password of the user.
        //[Required]
        //[StringLength(8, ErrorMessage = "Password is required")]
        public string Password { get; set; }

        // Gets or sets a value indicating whether the user has changed their initial password.
        public bool HasInitialPasswordChanged { get; set; } = false;
    }
}
