using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Errors;
using Core.Interfaces;
using Core.services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public  static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServiceExtension(this IServiceCollection services
        , IConfiguration config)
        {
            services.AddDbContext<BikesDbContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("con"));
            });
        
            services.AddScoped<ITokenService,TokenServices>();
            services.AddScoped<IBikeRepository,BikesRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(Options =>
                {
                    Options.InvalidModelStateResponseFactory = actionContext =>
            {
                    var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);

                };

                });
                services.AddCors(opt=>
                {
                opt.AddPolicy("CorsPolicy",policy=>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                    });
                });
            return services;
        }
        
    }
}