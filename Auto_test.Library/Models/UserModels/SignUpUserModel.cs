using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Auto_test.Library.Models.UserModels;
public class SignUpUserModel : UserParent
{

    [Display(Name = "Rasmingizni yuklang", Description = "Bu maydon majburiy emas!")]
    public IFormFile? ImagePath { get; set; }
}
