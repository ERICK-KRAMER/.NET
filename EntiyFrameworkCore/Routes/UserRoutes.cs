
using EntiyFrameworkCore.Data;
using EntiyFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EntiyFrameworkCore.Routes
{
    public static class UserRoutes
    {
        public static void AddRoutesUser(this WebApplication app)
        {
            app.MapGet("/", () => "Hello World");

            var user = app.MapGroup("/user");

            // Rota para listar todos os usuários
            user.MapGet("", async (AppDbContext context) =>
            {
                return await context.Users.ToListAsync();
            });

            // Rota para adicionar um novo usuário
            user.MapPost("", async (User user, AppDbContext context) =>
            {
                User userAlreadyExist = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

                if (userAlreadyExist != null)
                    return Results.Conflict("User already exists!");

                await context.AddAsync(user);
                await context.SaveChangesAsync();
                return Results.Ok(user);
            });

            // Rota para buscar um usuário por ID
            user.MapGet("{id}", async (Guid id, AppDbContext context) =>
            {
                User user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return Results.NotFound("User not found!");

                return Results.Ok(user);
            });
        }
    }
}