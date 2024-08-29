using EntiyFrameworkCore.Data;
using EntiyFrameworkCore.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

UserRoutes.AddRoutesUser(app);

app.Run();