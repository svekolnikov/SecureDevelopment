using Lesson1.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using NotesApi.DAL.Interfaces.Repositories;
using NotesApi.DAL.Repositories;
using NotesApi.Services;
using NotesApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//Repositories
services.AddScoped(typeof(IRepository<>), typeof(RepositoryEf<>));
services.AddScoped(typeof(IRepositoryEf<>), typeof(RepositoryEf<>));
services.AddScoped<INotesRepositoryDapper, NotesNotesRepositoryDapper>();

services.AddScoped(typeof(INotesManager<>), typeof(NotesManager<>));

//Database
services.AddDbContext<NotesDb>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
