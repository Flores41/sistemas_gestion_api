using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Masters
{
    public class RangeDto :AuditoriaDto
    {
        public int? iid_range { get; set; }
        public string? vname_range { get; set; }
        public string? vdescription_range { get; set; }
        public string? vcode_range { get; set; }        
        public int? ihours_normal_range { get; set; }
        public int? ihours_vip_range { get; set; }
        public string? vtime_agenda_range { get; set; }
        public decimal? dsupport_min_required { get; set; }

    }
}
