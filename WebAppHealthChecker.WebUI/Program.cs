using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Encodings.Web;
using WatchDog;
using WebAppHealthChecker.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplicationServices()
    .RegisterInfrastructureServices()
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterPresentationServices()
    .AddControllersWithViews();


var app = builder.Build();

ConfigureMiddlewares(app, app.Environment);

app.Run();


void ConfigureMiddlewares(WebApplication app, IHostEnvironment env)
{
    if (!env.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();


    app.UseStatusCodePages();

    app.UseStaticFiles();

    app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

    app.UseAuthentication();

    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    //app.UseWatchDogExceptionLogger();

    //app.UseWatchDog(opt =>
    //{
    //    opt.WatchPageUsername = "admin";
    //    opt.WatchPagePassword = "admin";
    //});
}