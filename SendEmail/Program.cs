using SendEmail.Models;
using SendEmail.Service;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var user = new SendEmailRequest("to@example.com", "Hello world", "<h1>Hello World</h1>");
    EmailService.SendEmail(user.Recipient, user.Subject, user.Body);

    return Results.Ok("E-mail foi enviado!");
});

app.Run();
