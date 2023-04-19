using Auto_test.Library.Models.Tickets;
using Auto_test.Library.Models.UserModels;
using Autotest_MVC_2.Services.QuestionServices;
using Autotest_MVC_2.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Autotest_MVC_2.Controllers;

public class RegistrationController : Controller
{
    private readonly UserService _userService;
    private readonly QuestionService _questionService;

    public RegistrationController(UserService userService, QuestionService questionService)
    {
        _userService = userService;
        _questionService = questionService;
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpUserModel signUpUserModel)
    {
        if (!ModelState.IsValid)
        {
            return View(signUpUserModel);
        }

        var email = _userService.DbContext.AllUsers.FirstOrDefault(u => u.UserEmailOrPhone == signUpUserModel.UserEmailOrPhone);

        var userName = _userService.DbContext.AllUsers.FirstOrDefault(u => u.UserName == signUpUserModel.UserName);


        if (userName != null && email != null)
        {
            ViewBag.UserNameHas = true;
            ViewBag.UserEmail = true;

            TempData["UserNameHas"] = true;
            TempData["UserEmail"] = true;

            return RedirectToAction(nameof(SignUp));
        }

        if (email != null)
        {
            ViewBag.UserEmail = true;

            TempData["UserEmail"] = true;
            return RedirectToAction(nameof(SignUp));
        }

        if (userName != null)
        {
            ViewBag.UserNameHas = true;
            TempData["UserNameHas"] = true;
            return RedirectToAction(nameof(SignUp));
        }

        ViewBag.UserNameHas = false;
        ViewBag.UserEmail = false;

        var user = new User()
        {
            UserId = Guid.NewGuid().ToString(),
            UserName = signUpUserModel.UserName,
            UserEmailOrPhone = signUpUserModel.UserEmailOrPhone,
            UserPassword = signUpUserModel.UserPassword,
            ImagePath = await SavePhotoPathAsync(signUpUserModel.ImagePath),
            IsExam = false,
            ExamCurrentTicketIndex = -1,
            UserTickets = new List<Ticket>(),
        };

        CreateUserTickets(user);

        HttpContext.Session.SetString("UserId", user.UserId);

        _userService.DbContext.AllUsers!.Add(user);
        _userService.DbContext.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    private void CreateUserTickets(User user)
    {
        var questionCount = _questionService.QuestionCount;
        var ticketCount = _questionService.TicketCount;

        for (var i = 0; i < ticketCount; i++)
        {
            var startIndex = i * questionCount + 1;

            user.UserTickets!.Add(new Ticket()
            {
                TicketIndex = i,
                CurrentQuestionIndex = startIndex,
                StarQuestionIndex = startIndex,
                QuestionsCount = questionCount
            });
        }
    }

    public async Task<string> SavePhotoPathAsync(IFormFile? ifFormFile)
    {
        var directoryPath = Path.Combine("wwwroot", "UserImages");

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        if (ifFormFile == null)
            return $"{Path.Combine("\\UserImages", "nophoto2.jpeg")}";

        var fileName = Guid.NewGuid() + ".jpg";

        using (MemoryStream ms = new())
        {
            await ifFormFile.CopyToAsync(ms);

            var photoPath = Path.Combine("wwwroot\\UserImages", fileName);

            await System.IO.File.WriteAllBytesAsync(photoPath, ms.ToArray());
        }

        return Path.Combine("\\UserImages", fileName);
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignIn(SignInUserModel signInUserModel)
    {
        var user = _userService.DbContext.AllUsers!.FirstOrDefault(
            u => u.UserEmailOrPhone == signInUserModel.UserEmailOrPhone
            && u.UserPassword == signInUserModel.UserPassword);

        if (user == null)
            return RedirectToAction(nameof(SignUp));

        var userId = UserService.UserIdCookieKey;

        HttpContext.Session.SetString(userId, user.UserId!);

        return RedirectToAction("Profile", "User");
    }

    public IActionResult CheckHasUser(bool check = true)
    {
        ViewBag.CheckHasUser = check;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(SignInUserModel signInUserModel)
    {
        var isHasUser = await _userService.DbContext.AllUsers!.AnyAsync(
            u => u.UserEmailOrPhone == signInUserModel.UserEmailOrPhone
                 && u.UserPassword == signInUserModel.UserPassword);

        if (isHasUser)
        {
            return View();
        }

        return RedirectToAction(nameof(CheckHasUser), new { check = false });
    }

    [HttpPost]
    public async Task<IActionResult> Update(SignUpUserModel signInUserModel)
    {
        var user = _userService.GetCurrentUser(HttpContext);

        if (user == null)
            return RedirectToAction(nameof(CheckHasUser));

        user.UserId = user.UserId;
        user.ImagePath = await SavePhotoPathAsync(signInUserModel.ImagePath);
        user.UserName = signInUserModel.UserName;
        user.UserEmailOrPhone = signInUserModel.UserEmailOrPhone;
        user.UserPassword = signInUserModel.UserPassword;

        _userService.DbContext.SaveChanges();

        ViewBag.Message = false;

        return RedirectToAction("Profile", "User");
    }


    public IActionResult LogOut()
    {
        const string userId = UserService.UserIdCookieKey;
        HttpContext.Session.Remove(userId);
        HttpContext.Response.Cookies.Delete(userId);
        return RedirectToAction("SignIn");
    }

}