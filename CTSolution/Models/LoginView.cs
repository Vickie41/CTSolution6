using System.ComponentModel.DataAnnotations;

namespace CTSolution.Models
{
    public class LoginView
    {
        
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        
    }
}
