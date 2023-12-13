using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites.Identity;
using infrastructure.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();

            services.AddDbContext<BikesDbContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("con"));
            });
            services.AddDbContext<AppIdentityDbContext>(opt=>{
                opt.UseSqlite(config.GetConnectionString("Idcon"));
            });
            //after migration below code
            services.AddIdentityCore<AppUser>(opt=>{
 

            }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication();
            services.AddAuthorization();
            return services;
        }
        
    }
}