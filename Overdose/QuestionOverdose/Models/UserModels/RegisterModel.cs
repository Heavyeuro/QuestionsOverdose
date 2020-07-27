using System.ComponentModel.DataAnnotations;

namespace QuestionOverdose.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
