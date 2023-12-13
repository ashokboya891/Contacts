using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;

namespace infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
         public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user=new AppUser
                {
                    DisplayName="Ram",
                    Email="ram@bha.com",
                    UserName="ram@bha.com",
                    Details=new Details
                    {
                        FirstName="Ram",
                        LastName="seeta",
                        Street="rajamandiram",
                        City="ayodya",
                        State="UP",
                        ZipCode="218343"
                    }

                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
    }
    }
}