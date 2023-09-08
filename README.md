### Down Notifier(WebApp Health Checker)

This repository was created for doing the Down Notifier coding-challenge.

# Problem description
You are developing a web application to monitor target applications’ health. It takes a URL as input and
periodically checks whether it’s up or not. It sends a notification message when a request to the URL
returns a response code other than 2XX. 

### Solution Objectives

I used MediatR for CQRS +  Clean Architecture To prepare this Solution and
 * ASP.NET Core MVC and bootstrap for UI
 * AJAX for refresh data-table(bootstrap-table) and load modal forms
 * Entity Framework ORM and localdb database
 * Elmah for logging errors(/elmah)
 * Register and login form for support multiple user accounts
 * You can easily implement the INotificationService interface to have more notification types

> WebAppHealthChecker.WebUI.ConfigureServices.cs

`services.AddScoped<INotificationService, EmailService>();
//Add more INotificationService here`

>MailService need configs

   
