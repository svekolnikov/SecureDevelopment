using DebitCardApi.DAL.DataContext;
using DebitCardApi.DAL.Interfaces.Repositories;
using DebitCardApi.DAL.Repositories;
using DebitCardApi.Services;
using DebitCardApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//Repositories
services.AddScoped(typeof(IRepository<>), typeof(RepositoryEf<>));
services.AddScoped(typeof(IRepositoryEf<>), typeof(RepositoryEf<>));
services.AddScoped<IRepositoryDapper, RepositoryDapper>();

services.AddScoped(typeof(IDebitCardsManager<>), typeof(DebitCardsManager<>));

//Database
services.AddDbContext<DebitCardsDb>(opt => 
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
