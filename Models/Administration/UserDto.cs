using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using static System.Net.Mime.MediaTypeNames;

namespace Models.Administration
{
    public class UserDto : AuditoriaDto
    {
        public int? iid_user { get; set; }
        public string? vcode { get; set; }
        public string? vfirst_name { get; set; }
        public string? vlast_name { get; set; }
        public string? vfull_names { get; set; }
        public string? vemail { get; set; }
        public int? inumber_document { get; set; }
        public int? itype_document { get; set; }
        public int? iphone { get; set; }
        public string? vphone { get; set; }

        public string? vaddress { get; set; }
        public int? iid_addrres{ get; set; }
        public int? iid_profile { get; set; }
        public string? vname_profile { get; set; }
        public string? vpassword { get; set; }

    }

    public class UserByParameterDto : AuditoriaDto
    {
        public int? iid_user { get; set; }
        public string? vfirst_name_user { get; set; }
        public string? vlast_name_user { get; set; }
        public string? vfull_names_user { get; set; }


        public int? iphone_user { get; set; }
        public string? vphone_user { get; set; }
        public string? vpassword_user { get; set; }


        public int? iid_profile_user { get; set; }
        public string? vname_profile_user { get; set; }

        public string? vnumber_document { get; set; }
        
    }

}
