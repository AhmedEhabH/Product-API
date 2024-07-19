using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class RegisterDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        [MinLength(5, ErrorMessage = "Min Length 5 characters")]
        public required string DisplayName { get; set; }
        [RegularExpression(@"(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$",
            ErrorMessage = "It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters.")]
        public required string Password { get; set; }
    }
}
