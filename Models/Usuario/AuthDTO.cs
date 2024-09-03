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
        public List<MenuUsuario> menu { get; set; }
    }


    public class MenuUsuario
    {
        public int? iid_module { get; set; }
        public string? vname_module { get; set; }
        public string? vdescription_module { get; set; }
        public int? iorder_module { get; set; }
        public string? vurl_module { get; set; }
        public string? vicon_module { get; set; }

        public int? iid_option { get; set; }
        public string? vname_option { get; set; }
        public string? vdescription_option { get; set; }
        public int? iorder_option { get; set; }
        public string? vicon_option { get; set; }
        public string? vurl_option { get; set; }

        public bool? baccess_view { get; set; }
        public bool? baccess_create { get; set; }
        public bool? baccess_update { get; set; }
        public bool? baccess_delete { get; set; }
        public bool? bsub_menu_module { get; set; }

        

    }

}
