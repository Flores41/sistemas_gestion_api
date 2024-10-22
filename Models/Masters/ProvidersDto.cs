using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Masters
{
    public class ProvidersDto : AuditoriaDto
    {
        public int? iid_provider { get; set; }
        public string? vcode_provider { get; set; }
        public string? vruc_provider { get; set; }
        public string? vrazon_social_provider { get; set; }


        public string? vtype_provider { get; set; }
        public string? vdescription_type_provider { get; set; }


        public string? vphone_number_provider { get; set; }
        public string? vemail_provider { get; set; }
        public string? vweb_address_provider { get; set; }
        public int? irating_provider { get; set; }


        public string? iubigeo_provider { get; set; }
        public string? vubigeo_provider { get; set; }


        public string? iid_departament_provider { get; set; }
        public string? vname_departament_provider { get; set; }


        public string? iid_province_provider { get; set; }
        public string? vname_province_provider { get; set; }


        public string? iid_district_provider { get; set; }
        public string? vname_district_provider { get; set; }
    }

    public class RegisterProvidersDto : AuditoriaDto
    {
        public int? iid_provider { get; set; }
        public string? vcode_provider { get; set; }
        public string? vruc_provider { get; set; }
        public string? vrazon_social_provider { get; set; }
        public string? vtype_provider { get; set; }
        public string? vphone_number_provider { get; set; }
        public string? vemail_provider { get; set; }
        public string? vweb_address_provider { get; set; }
        public int? irating_provider { get; set; }
        public string? iubigeo_provider { get; set; }
    }
}
