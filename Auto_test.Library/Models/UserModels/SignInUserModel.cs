using System.ComponentModel.DataAnnotations;

namespace Auto_test.Library.Models.UserModels;
public class SignInUserModel
{

    [Required(ErrorMessage = "Telefon yoki Email ni kirirting!")]
    public string? UserEmailOrPhone { get; set; }


    [Required(ErrorMessage = "Parolni kiriting!")]
    public string? UserPassword { get; set; }

}
