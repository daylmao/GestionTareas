using GestionTareas.Middleware;
using GestionTareas.Core.Application;
using GestionTareas.Infraestructure.Persistence;
using Microsoft.AspNetCore.Identity;
using GestionTareas.Infraestructure.Identity.Entities;
using GestionTareas.Infraestructure.Identity.Seeds;
using GestionTareas.Infraestructure.Identity;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceMethod(config);
builder.Services.AddApplicationMethod();
builder.Services.AddIdentityMethod(config);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

	try
	{
		var userManager = services.GetRequiredService<UserManager<User>>();
		var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();
		await DefaultBasicRoles.SeedAsync(userManager, rolManager);
		await DefaultRoles.SeedAsync(userManager, rolManager);
	}
	catch (Exception )
	{
        

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<Middleware>();
app.MapControllers();

app.Run();
