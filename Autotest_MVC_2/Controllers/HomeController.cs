using Autotest_MVC_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Autotest_MVC_2.Services.QuestionServices;

namespace Autotest_MVC_2.Controllers;

public class HomeController : Controller
{
    private readonly QuestionService _questionService;

    public HomeController(QuestionService questionService)
    {
        _questionService = questionService;
    }

    public IActionResult Index(string language = "uzlotin")
    {
        return View();
    }


    public IActionResult About()
    {
        return View();
    }

    public IActionResult IsRegister()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}