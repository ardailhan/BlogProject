using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels.Auth.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username can not be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [DataType(DataType.Password)]
        [RegularExpression("[a-zA-Z0-9]{8,16}",ErrorMessage ="Your password should be at least 8 charachter, at most 16 charachter")]
        public string Password { get; set; }
    }
}
