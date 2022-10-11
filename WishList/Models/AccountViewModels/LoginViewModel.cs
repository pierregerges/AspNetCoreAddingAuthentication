using System.ComponentModel.DataAnnotations;

namespace WishList.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress (ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    
    }
}
