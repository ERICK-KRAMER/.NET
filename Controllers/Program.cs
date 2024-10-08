using Controllers.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<EntityFrameworkContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();