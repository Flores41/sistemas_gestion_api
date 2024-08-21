using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Administration
{
    public class ProfileDTO : AuditoriaDto
    {
        public int? iid_profile { get; set; }
        public string? vcode_profile { get; set; }
        public string? vname_profile { get; set; }
        public string? vdescription_profile { get; set; }
        
    }

    public class ProfileAccessDTO : OptionByModuleDto
    {
        public int? iid_profile_access { get; set; }
        public int? iid_profile { get;set ; }

        public string? vcode_profile { get; set; }
        public string? vname_profile { get; set; }
        public string? vdescription_profile { get; set; }

        public bool? baccess_view { get; set; }
        public bool? baccess_create { get; set; }
        public bool? baccess_update { get; set; }
        public bool? baccess_delete { get; set; }

    }

    public class ProfileAccessRegisterDTO  : AuditoriaDto
    {
        public int? iid_profile_access { get; set; }
        public int? iid_profile { get; set; }
        public string? vname_profile { get; set; }
        public string? vdescription_profile { get; set; }
        public List<ProfileAccessDTO> lstOptions { get; set; }
    }


}
