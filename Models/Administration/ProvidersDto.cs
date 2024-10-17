using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Administration
{
    public class ProvidersDto : AuditoriaDto
    {
        public int? iid_provider { get; set; }
        public string? vcode_provider { get; set; }
        public string? vruc_provider { get; set; }
        public string? vrazon_social_provider { get; set; }


        public int? itipe_service_provider { get; set; }
        public string? vtipe_service_provider { get; set; }


        public string? vphone_number_provider { get; set; }
        public string? vemail_provider { get; set; }
        public string? vweb_address_provider { get; set; }
        public int? irating_provider { get; set; }


        public int? iubigeo_provider { get; set; }


        public int? iid_departament_provider { get; set; }
        public string? vname_departament_provider { get; set; }


        public int? iid_province_provider { get; set; }
        public string? vname_province_provider { get; set; }


        public int? iid_district_provider { get; set; }
        public string? vname_district_provider { get; set; }
    }

    public class RegisterProvidersDto : AuditoriaDto
    {
        public int? iid_provider { get; set; }
        public string? vcode_provider { get; set; }
        public string? vruc_provider { get; set; }
        public string? vrazon_social_provider { get; set; }
        public int? itipe_service_provider { get; set; }
        public string? vphone_number_provider { get; set; }
        public string? vemail_provider { get; set; }
        public string? vweb_address_provider { get; set; }
        public int? irating_provider { get; set; }
        public int? iubigeo_provider { get; set; }
    }
}
