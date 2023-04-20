using Autotest_MVC_2.DataDB;
using Autotest_MVC_2.Services.QuestionServices;
using Autotest_MVC_2.Services.UserServices;
using Serilog;


namespace Autotest_MVC_2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var logger = new LoggerConfiguration().WriteTo.File("Error.txt").CreateLogger();

        // Add services to builder.Services.AddControllersWithViews();  // ��������� ������� � builder.Services.AddControllersWithViews()
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();
        builder.Services.AddMemoryCache();
        
        builder.Logging.AddSerilog(logger);

        builder.Services.AddDbContext<AppDbContext>();

        builder.Services.AddTransient<UserService>();

        builder.Services.AddTransient<QuestionService>();




        var app = builder.Build();

        // Configure the HTTP request pipeline   // ��������� �������� HTTP - ��������
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            // The default HSTS value is 30 days,  // �������� HSTS �� ��������� - 30 ����
            // You may want to change this for production scenarios,  // �� ������ ������� ��� ��� ������� ���������
            // see http://aka.ms/aspnetcore-hsts  // ��������� ���������� �������� �� http://aka.ms/aspnetcore-htst 
            app.UseHsts();
        }

        //  app.Services.GetRequiredService<AppDbContext>().Database.Migrate();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}