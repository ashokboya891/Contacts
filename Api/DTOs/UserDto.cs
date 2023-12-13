using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs
{
    public class UserDto
    {
        public string  Email{set;get;}
        public string DisplayName{set;get;}
        public string Token{get;set;}

    }
}