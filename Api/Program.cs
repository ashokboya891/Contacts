using Api.Extensions;
using Core.Entites.Identity;
using Core.Interfaces;
using Core.services;
using infrastructure.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentityService(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ITokenService,TokenServices>();
builder.Services.AddScoped<IBikeRepository,BikesRepository>();

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  //after adding mappingprofile file automapper this has to add
builder.Services.AddCors(opt =>
                {
                    opt.AddPolicy("CorsPolicy", policy =>
                        {
                            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                        });
                });
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
var identityContext=services.GetRequiredService<AppIdentityDbContext>();
var userManager=services.GetRequiredService<UserManager<AppUser>>();
var logger=services.GetRequiredService<ILogger<Program>>();
try{
await identityContext.Database.MigrateAsync();
await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch(Exception ex)
{
    logger.LogError(ex,"An Error occured during migration");
}
app.Run();
