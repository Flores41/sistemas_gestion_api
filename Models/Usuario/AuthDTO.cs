using Models.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Usuario
{
    public class LoginDTO
    {
        [Required]
        public string vnumber_document { get; set; }
        [Required]
        public string vpassword_user { get; set; }
        public string? GoogleToken { get; set; }

    }


    public class LoginRecuperacionDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string codigo { get; set; }

    }

    public class AuthenticationResponse
    {
        public UserByParameterDto userdata { get; set; }
        public object menu { get; set; }
    }

}
