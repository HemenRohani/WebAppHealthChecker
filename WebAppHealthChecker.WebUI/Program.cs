using ElmahCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppHealthChecker.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplicationServices()
    .RegisterInfrastructureServices()
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterPresentationServices()
    .AddControllersWithViews();


var app = builder.Build();

ConfigureMiddlewares(app, app.Environment);

ConfigureDB(app);

app.Run();


void ConfigureMiddlewares(WebApplication app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        //app.UseDeveloperExceptionPage();
        app.UseElmahExceptionPage();
    }

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

    app.UseElmah();
}

void ConfigureDB(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
}