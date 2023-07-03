using Auto_test.Library.Models.Tickets;
using Auto_test.Library.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Autotest_MVC_2.DataDB;

public class AppDbContext : DbContext
{
    public DbSet<User>? AllUsers { get; set; }
    public DbSet<Ticket>? Tickets { get; set; }
    public DbSet<TicketQuestionAnswer>? TicketQuestionAnswers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
/*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = "Server = (localdb)\\MSSQLLocalDB;" +
                      "Database = AutoTest_db;" +
                      "Trusted_Connection = True;";

        optionsBuilder.UseSqlServer(path);
    }*/
}



