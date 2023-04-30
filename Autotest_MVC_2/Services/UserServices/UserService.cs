using Auto_test.Library.Models.UserModels;
using Autotest_MVC_2.DataDB;
using Microsoft.EntityFrameworkCore;

namespace Autotest_MVC_2.Services.UserServices;

public  class UserService
{
    public const string UserIdCookieKey = "UserId";

    public AppDbContext DbContext;

    public UserService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void ClearDb()
    {
        var users = DbContext.AllUsers;
        var ticketAnswers = DbContext.TicketQuestionAnswers;
        DbContext.RemoveRange(users!);
        DbContext.RemoveRange(ticketAnswers!);
        DbContext.SaveChanges();
    }

    public User? GetCurrentUser(HttpContext context)
    {
        if (string.IsNullOrEmpty(context.Session.GetString(UserIdCookieKey)))
            return null;

        var userId = context.Session.GetString(UserIdCookieKey);
        var user = DbContext.AllUsers?.FirstOrDefault(x => x.UserId == userId);

        return user ?? null;
    }

    public  User? GetCurrentUserAndTickets(HttpContext context)
    {
        if (string.IsNullOrEmpty(context.Session.GetString(UserIdCookieKey)))
            return null;

        var userId = context.Session.GetString(UserIdCookieKey);

        var user = DbContext.AllUsers.Where(u => u.UserId == userId).
            Include(t => t.UserTickets).
            ThenInclude(a=> a.TicketAnswers).FirstOrDefault();

        return user ?? null;
    }
}