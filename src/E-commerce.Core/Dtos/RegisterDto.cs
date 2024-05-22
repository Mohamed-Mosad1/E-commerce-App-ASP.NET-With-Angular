using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min Length is 3 character")]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",  
            ErrorMessage = "Password It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters. The sequence of the characters is not important. This expression follows the above 4 norms specified by microsoft for a strong password.")]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
