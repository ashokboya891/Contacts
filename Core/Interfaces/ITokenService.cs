using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites.Identity;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);

    }
}