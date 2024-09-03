using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models.Administration
{
    public class ModuleDto :AuditoriaDto
    {
        public int? iid_module { get; set; }
        public string? vname_module { get; set; }
        public string? vdescription_module { get; set; }
        public string? vurl_module { get; set; }
        public string? vicon_module { get; set; }
        public int? iorder_module { get; set; }
        public Boolean? bvisible_module { get; set; }
        public Boolean? bsub_menu_module { get; set; }

    }

    public class OptionDto : AuditoriaDto
    {
        public int? iid_option { get; set; }
        public string? vname_option { get; set; }
        public string? vdescription_option { get; set; }
        public string? vurl_option { get; set; }
        public string? vicon_option { get; set; }
        public int? iid_module { get; set; }
        public int? iorder_option { get; set; }

        public Boolean? bvisible_option { get; set; }
                
    }

    public class OptionByModuleDto : AuditoriaDto
    {
        public int? iid_module { get; set; }
        public string? vname_module { get; set; }
        public string? vdescription_module { get; set; }
        public int? iorder_module { get; set; }
        public string? vicon_module { get; set; }
        public string? vurl_module { get; set; }


        public int? iid_option { get; set; }
        public string? vname_option { get; set; }
        public string? vdescription_option { get; set; }
        public int? iorder_option { get; set; }
        public string? vicon_option { get; set; }
        public string? vurl_option { get; set; }

        public bool? bsub_menu_module { get; set; }


    }


}
