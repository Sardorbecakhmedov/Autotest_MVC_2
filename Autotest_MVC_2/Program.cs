using Autotest_MVC_2.DataDB;
using Autotest_MVC_2.Services.QuestionServices;
using Autotest_MVC_2.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Autotest_MVC_2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Xatolarni topib faylga yozadi
        var logger = new LoggerConfiguration().WriteTo.File("Error.txt").CreateLogger();
        builder.Logging.AddSerilog(logger);

        // Add services to builder.Services.AddControllersWithViews();  // Добавляем сервисы в builder.Services.AddControllersWithViews()
        builder.Services.AddControllersWithViews();

        builder.Services.AddSession();
        builder.Services.AddMemoryCache();

        // Xatolarni topib faylga yozadi

        builder.Services.AddDbContext<AppDbContext>(config =>
        {
            string path = "Server = (localdb)\\MSSQLLocalDB;" +
                          "Database = AutoTest_db;" +
                          "Trusted_Connection = True;";

            config.UseSqlServer(path);
        });

        builder.Services.AddTransient<AppDbContext>();

        builder.Services.AddTransient<UserService>();

        builder.Services.AddTransient<QuestionService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline   // Настройте конвейер HTTP - запросов
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            // The default HSTS value is 30 days,  // Значение HSTS по умолчанию - 30 дней
            // You may want to change this for production scenarios,  // Вы можете изменит это для рабочих сценариев
            // see http://aka.ms/aspnetcore-hsts  // Подробное информацию смотрите на http://aka.ms/aspnetcore-htst 
            app.UseHsts();
        }

        // app.Services.GetRequiredService<AppDbContext>().Database.Migrate();

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