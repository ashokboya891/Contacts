using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs
{
    public class RegisterDto
    {
        [Required]

        public string DisplayName{set;get;}
        [Required]
        [EmailAddress]
        public string Email{set;get;}
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",ErrorMessage ="Password must have 1 UpperCase,1 LowerCase,1 Number,1 Alphanumeric atleats 6 ")]
        public string Password{set;get;}
    }
}