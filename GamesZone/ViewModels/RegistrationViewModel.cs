using System.ComponentModel.DataAnnotations;

namespace GamesZone.ViewModels
{
    public class RegistrationViewModel
    {


        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirmed { get; set; }


    }
}
