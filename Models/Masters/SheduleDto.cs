using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Masters
{
    public class SheduleDto : AuditoriaDto
    {
        public int? iid_shedule_weekly { get; set; }
        public string? vname_shedule_weekly { get; set; }
        public string? vdescription_shedule_weekly { get; set; }
        public int? iid_day_shedule_weekly { get; set; }
        public string? vhour_shedule_weekly { get; set; }
        public int? itype_stream { get; set; }
        public int? iid_user { get; set; }
        public int? iid_comunity { get; set; }
        public int? iid_week_shedule_weekly { get; set; }

        
        public string? vurl_channel_twitch { get; set; }
        public string? vchannel_twitch { get; set; }
    }
    public class SheduleWeekDto : AuditoriaDto
    {
        public int? iid_shedule_weekly { get; set; }
        public string? vzone_time_user { get; set; }
        public string? vzone_time_mx { get; set; }

        public List<StreamInfoDto> vday_monday { get; set; }
        public List<StreamInfoDto> vday_tuesday { get; set; }
        public List<StreamInfoDto> vday_wednesday { get; set; }
        public List<StreamInfoDto> vday_thursday { get; set; }
        public List<StreamInfoDto> vday_friday { get; set; }
        public List<StreamInfoDto> vday_saturday { get; set; }
        public List<StreamInfoDto> vday_sunday { get; set; }

    }

    public class StreamInfoDto
    {
        public string? vname_channel { get; set; }
        public string? vurl_channel { get; set; }
        public int? itype_stream { get; set; }

    }
}
