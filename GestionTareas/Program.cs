using GestionTareas.Context;
using GestionTareas.Mapper;
using GestionTareas.Middleware;
using GestionTareas.Core.Application.Service;
using Microsoft.EntityFrameworkCore;
using GestionTareas.Core.Application.Interfaces.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddDbContext<GestorTareasContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GestionTareasConnection")));
builder.Services.AddScoped<ITareaService, TareaService>();  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<Middleware>();
app.MapControllers();

app.Run();
