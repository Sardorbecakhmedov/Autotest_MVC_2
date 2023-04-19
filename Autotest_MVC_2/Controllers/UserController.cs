using Autotest_MVC_2.Services.QuestionServices;
using Autotest_MVC_2.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Autotest_MVC_2.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;
    private readonly QuestionService _questionService;

    public UserController(UserService userService, QuestionService questionService)
    {
        _userService = userService;
        _questionService = questionService;
    }

    public async Task<IActionResult> EditLanguage(string language = "uzlotin")
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);
        await _questionService.GetAllQuestionsAsync(language);

        if (user != null)
        {
            await _questionService.GetAllQuestionsAsync(language);
            return RedirectToAction("ShowTickets", "Question");
        }

        return RedirectToAction("IsRegister", "Home");
    }


    public IActionResult Profile()
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);

        if (user != null)
            return View(user);

        ViewBag.TotalQuestionCount = _questionService.TotalQuestionCount;
        return RedirectToAction("IsRegister", "Home");
    }

    public IActionResult ClearDb()
    {
        _userService.ClearDb();
        return View();
    }

    public IActionResult Rating()
    {
        var topUsers = _userService.DbContext.AllUsers?
            .OrderByDescending(u => u.TotalCorrectAnswerCount)
            .Take(10)
            .ToList();

        ViewBag.Users = topUsers!;

        return View();
    }
}