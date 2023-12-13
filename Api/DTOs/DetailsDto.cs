using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs
{
    public class DetailsDto
    {
                [Required]
        
        public string FirstName{set;get;}
        [Required]

        public string LastName{set;get;}
        [Required]

        public string Street{set;get;}
        [Required]

        public string City{set;get;}
        [Required]

        public string State{set;get;}
        [Required]

        public string ZipCode{set;get;}
    }
}