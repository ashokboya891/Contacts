using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entites.Identity
{
    public class AppUser:IdentityUser
    {
        public string DisplayName{set;get;}
        public Details Details{set;get;}
    }
}