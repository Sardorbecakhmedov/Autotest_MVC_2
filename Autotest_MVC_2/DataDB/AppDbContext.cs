using Auto_test.Library.Models.Tickets;
using Auto_test.Library.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Autotest_MVC_2.DataDB;

public class AppDbContext : DbContext
{
    public DbSet<User>? AllUsers { get; set; }
    public DbSet<Ticket>? Tickets { get; set; }
    public DbSet<TicketQuestionAnswer>? TicketQuestionAnswers { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = @"Server = (localdb)\MSSQLLocalDB;" +
                        "Database = users;" +
                        "Trusted_Connection = True;";

        optionsBuilder.UseSqlServer(path);
    }
}



