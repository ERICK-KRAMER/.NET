using JWT.Models;
using JWT.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", (TokenService service)
=> service.GenerateToken(user: new User(Guid.NewGuid(), "ERICKKRAMER@HOTMAIL.COM", "ERICKKRAMER123@", [
    "student", "premium"
])));

app.Run();
