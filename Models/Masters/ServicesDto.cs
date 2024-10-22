using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Masters
{
    public class ServicesDto : AuditoriaDto
    {
		public int? iid_service { get; set; }
		public string? vcode_service { get; set; }
        public string? vname_service { get; set; }
        public string? vdescription_service { get; set; }
        public string? vprice_service { get; set; }
        public string? lst_references_service { get; set; }
        public int? iquantity_service { get; set; }


        public int? iid_provider_service { get; set; }
        public string? vprovider_service { get; set; }


        public int? itype_price_service { get; set; }
        public string? vtype_price_service { get; set; }


        public string? ilst_type_events_service { get; set; }
        public string? vlst_type_events_service { get; set; }

    }

    public class ServicesRegisterDto : AuditoriaDto
    {
        public int? iid_service { get; set; }
        public string? vname_service { get; set; }
        public string? vdescription_service { get; set; }

 

        public int? iquantity_service { get; set; }

        public int? iid_provider_service { get; set; }



        public string? ilst_type_events_service { get; set; }


        public string? vprice_service { get; set; }
        public int? itype_price_service { get; set; }


        public string? lst_references_service { get; set; }

    }
}
