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
      //  string path = @"server=sql.bsite.net\MSSQL2016;Database=sardorbek2015_AutotestDb;user=sardorbek2015_AutotestDb;password=s89251411965";
        string path = @"Server=SQL8005.site4now.net;
                        Database=db_a986c3_autotest7;
                        User=db_a986c3_autotest7_admin;
                        Password=s89251411965";

        optionsBuilder.UseSqlServer(path);
    }
}



