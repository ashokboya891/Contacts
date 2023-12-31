using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entites.Identity
{
    public class Details
    {
        public int Id{set;get;}
        public string FirstName{set;get;}
        public string LastName{set;get;}
        public string Street{set;get;}
        public string City{set;get;}
        public string State{set;get;}
        public string ZipCode{set;get;}

        [Required]
        public string AppUserId{set;get;}
        public AppUser AppUser{set;get;}
    }
}