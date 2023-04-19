using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Autotest.Library.Models.UserModels;
public class SignUpUserModel : User
{

    [Display(Name = "Rasmingizni yuklang", Description = "Bu maydon majburiy emas!")]
    public IFormFile? ImagePath { get; set; }
}
