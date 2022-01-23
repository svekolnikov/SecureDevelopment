using System.Security.Claims;
using DebitCardApi;
using DebitCardApi.DAL.DataContext;
using DebitCardApi.DAL.IdentityContext;
using DebitCardApi.DAL.Interfaces.Repositories;
using DebitCardApi.DAL.Models.Identity;
using DebitCardApi.DAL.Repositories;
using DebitCardApi.Services;
using DebitCardApi.Services.Identity;
using DebitCardApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
services.AddDbContext<DataDbContext>(optionsAction =>
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Data")));

//Identity
services.AddDbContext<ApplicationDbContext>(optionsAction =>
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Identity")))
    .AddIdentity<ApplicationUser, ApplicationRole>(config =>
    {
        config.User.RequireUniqueEmail = true;
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequiredLength = 6;
        config.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(config =>
                {
                    config.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                            var userGuid = context.Principal.FindFirstValue(ClaimTypes.Name);
                            var user = await userManager.FindByIdAsync(userGuid);

                            if (user == null)
                            {
                                context.Fail("User not found");
                            }
                        }
                    };

                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = Constants.GetSymmetricSecurityKey(),

                        ValidateIssuer = true,
                        ValidIssuer = Constants.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = Constants.AUDIENCE,

                        ValidateLifetime = true,
                    };

                });

services.AddAuthorization(config =>
{
    config.AddPolicy("AdminPolicy", configPolicy =>
    {
        configPolicy.RequireClaim(ClaimTypes.Role, "admin");
    });

    config.AddPolicy("UserPolicy", configPolicy =>
    {
        configPolicy.RequireClaim(ClaimTypes.Role, "user");
    });

    config.AddPolicy("UsersAndAdminPolicy", configPolicy =>
    {
        configPolicy.RequireAssertion(x =>
            x.User.HasClaim(ClaimTypes.Role, "admin") ||
            x.User.HasClaim(ClaimTypes.Role, "user"));
    });
});

services.AddScoped<IJwtService, JwtService>();
services.AddScoped<IAccountsManager, AccountsManager>();
services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
