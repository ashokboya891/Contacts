using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites.Identity;
using infrastructure.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();

        
            services.AddDbContext<AppIdentityDbContext>(opt=>{
                opt.UseSqlite(config.GetConnectionString("Idcon"));
            });
            //after migration below code
            services.AddIdentityCore<AppUser>(opt=>{
 

            }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(Options =>
          {
            Options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
              ValidIssuer = config["Token:Issuer"],
              ValidateIssuer=true,
              ValidateAudience = false,
            };

          });

      services.AddAuthorization();
            return services;
        }
        
    }
}