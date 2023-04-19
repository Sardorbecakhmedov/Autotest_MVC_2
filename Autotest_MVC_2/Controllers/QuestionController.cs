using Auto_test.Library.Models.Tickets;
using Auto_test.Library.Models.UserModels;
using Autotest_MVC_2.Services.QuestionServices;
using Autotest_MVC_2.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Autotest_MVC_2.Controllers;

public class QuestionController : Controller
{
    private readonly UserService _userService;

    private readonly QuestionService _questionService;

    public QuestionController(UserService userService, QuestionService questionService)
    {
        _userService = userService;
        _questionService = questionService;
    }

    public IActionResult ShowTickets()
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);

        if (user != null)
        {
            return View(user);
        }

        return RedirectToAction("IsRegister", "Home");
    }

    public IActionResult Exam()
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);

        if (user == null)
            return RedirectToAction("IsRegister", "Home");

        user.IsExam = true;
        _userService.DbContext.SaveChanges();
        return View(user);
    }

    
    public IActionResult StartTicket(int ticketIndex)
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);

        if (user == null)
            return RedirectToAction("IsRegister", "Home");

        if (user.UserTickets?.Count <= ticketIndex)
            return RedirectToAction("Index", "Home");
              
        if(user.IsExam)
            user.ExamCurrentTicketIndex = ticketIndex;

        user.CurrentTicketIndex = ticketIndex;
       
        var ticket = user.UserTickets?.FirstOrDefault(x => x.TicketIndex ==  ticketIndex);

        if (ticket != null)
        {
            ticket.CreateDateTime = DateTime.Now;

            user.CurrentTicket = ticket;

            user.CurrentTicket.CreateDateTime = DateTime.Now;
            _userService.DbContext.SaveChanges();
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction(
            nameof(GetQuestion), new { questionId = user.CurrentTicket.StarQuestionIndex });
    }

    public IActionResult GetQuestion(int questionId, int? choicesIndex = null)
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);

        if (user == null)
            return RedirectToAction("IsRegister", "Home");

        if (questionId > user.CurrentTicketIndex * 10 + 10)
            return RedirectToAction(nameof(Result));

        var question = _questionService.Questions!.FirstOrDefault(t => t.Id == questionId);

        if (question == null)
            RedirectToAction("ShowTickets", new { language = "uzlotin" });

        ViewBag.Question = question!;
        ViewBag.IsAnswer = choicesIndex != null;

        if (choicesIndex != null)
        {
            var ticketAnswer = new TicketQuestionAnswer
            {
                UserId = user.UserId,
                ChoiceIndex = choicesIndex.Value,
                QuestionIndex = questionId,
                CorrectAnswerIndex = question!.Choices!.IndexOf(
                    question.Choices.First(answer => answer.Answer))
            };

            user.CurrentTicket!.TicketAnswers!.Add(ticketAnswer);

            _userService.DbContext.TicketQuestionAnswers!.Add(ticketAnswer);
            _userService.DbContext.SaveChanges();

            ViewBag.TicketAnswer = ticketAnswer;
        }

        if (user.CurrentTicket!.QuestionsCount == user.CurrentTicket.TicketAnswers!.Count)
        {
            user.IsCompletedTicketCount++;
            user.TotalCorrectAnswerCount += user.CurrentTicket.TicketAnswers.Count(a=>a.IsCorrectAnswer == true);
            _userService.DbContext.SaveChanges();
        }

        return View(user);
    }

    public IActionResult Result()
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);
        if (user == null)
            return RedirectToAction("IsRegister", "Home");

        ViewBag.IsExam = user.IsExam;
        user.IsExam = false;
        user.ExamCurrentTicketIndex = -1;
        _userService.DbContext.SaveChanges();

        return View(user);
    }

    public IActionResult DeleteExamData()
    {
        var user = _userService.GetCurrentUser(HttpContext);

        if (user == null)
            return RedirectToAction("IsRegister", "Home");

        user.IsExam = false;
        user.ExamCurrentTicketIndex = -1;
        _userService.DbContext.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult RemoveTicketAnswer()
    {
        var user = _userService.GetCurrentUserAndTickets(HttpContext);

        if (user == null)
            return RedirectToAction("SignUp", "Registration");

        user.IsCompletedTicketCount--;
        var ticket = user.CurrentTicket;

        _userService.DbContext.TicketQuestionAnswers!.RemoveRange(ticket.TicketAnswers);
        _userService.DbContext.AllUsers?.Update(user);
        _userService.DbContext.SaveChanges();

        return RedirectToAction(
            nameof(GetQuestion), new { questionId = ticket.StarQuestionIndex });
    }
}