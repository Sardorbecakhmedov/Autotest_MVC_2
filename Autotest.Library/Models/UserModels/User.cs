using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Autotest.Library.Models.UserModels;

public class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Ismingizni kirirting!")]
    public string UserName { get; set; }

    public string ImagePath { get; set; }

    [Required(ErrorMessage = "Telefon yoki Email ni kirirting!")]
    public string UserEmailOrPhone { get; set; }


    [Required(ErrorMessage = "Parolni kiriting!")]
    public string UserPassword { get; set; }

}