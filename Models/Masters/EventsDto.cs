using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Masters
{
    public class EventsDto : AuditoriaDto
    {
        public int? iid_event { get; set; }
        public string? vcode_event { get; set; }
        public string? vname_event { get; set; }
        public string? vdescription_event { get; set; }

        public decimal? dprice_event { get; set; }
        public DateTime? ddate_event { get; set; }


        public string? vlocation_event { get; set; }
        public string? vreference_event { get; set; }
        public string? vdocumentation_event { get; set; }


        public int? iid_departament_event { get; set; }
        public string? vname_departament_event { get; set; }


        public string? iid_province_event { get; set; }
        public string? vname_province_event { get; set; }


        public string? iid_district_event { get; set; }
        public string? vdescripcion_district_event { get; set; }


        public int? iid_client_event { get; set; }
        public string? vname_client_event { get; set; }


        public int? iid_user_in_charge_event { get; set; }
        public string? vname_in_charge_event { get; set; }


        public int? iid_type_event { get; set; }
        public string? vtype_event { get; set; }


        public int? istate_event { get; set; }
        public string? vstate_event { get; set; }


        public int? iid_service_event { get; set; }
        public string? vid_service_event { get; set; }


        public string? vid_type_service_event { get; set; }


        public int? iid_priority_event { get; set; }
        public string? vpriority_event { get; set; }
    }


    public class SaveEventsDTO : AuditoriaDto
    {

        public int? iid_event { get; set; }
        public string? vname_event { get; set; }
        public string? vdescription_event { get; set; }
        public DateTime? ddate_event { get; set; }

        public int? itype_event { get; set; }
        public int? iclient_event { get; set; }
        public int? iuser_in_charge_event { get; set; }


        public string? vlocation_event { get; set; }
        public string? vreference_event { get; set; }


        public int? ipriority_event { get; set; }
        public string? vdocumentation_event { get; set; }
        public decimal? dprice_event { get; set; }


        public string? vid_type_service_event { get; set; }
        public int? istate_event { get; set; }
        public string? vid_service_event { get; set; }

    }

}
