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
        public int? iid_department { get; set; }
        public int? iid_profile { get; set; }
        public string? vname_profile { get; set; }
        public string? vpassword { get; set; }
        public string? vurl_image { get; set; }
        public string? vfirebase_folder { get; set; }

        public int? iid_range_member { get; set; }
        public string? vrange_member { get; set; }        
        public string? vurl_channel_twitch { get; set; }
        public string? vchannel_twitch { get; set; }

        public int? iid_comunity { get; set; }
        public string? vname_comunity { get; set; }

        public int? iid_time_zone { get; set; }

        public string? vtime_zone { get; set; }
        public bool? buser_member { get; set; } 

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
        public bool? bis_member_user { get; set; }



        public int? iid_range_user { get; set; }
        public string? vrange_user { get; set; }
        public int? ihours_normal_range_user { get; set; }
        public int? ihours_vip_range_user { get; set; }
        public string? vtime_agenda_range_user { get; set; }


        
        public string? vchannel_twitch_user { get; set; }
        public string? vurl_channel_twitch_user { get; set; }


        public int? iid_comunity_user { get; set; }
        public string? vname_comunity_user { get; set; }
        public string? vurl_image_comunity_user { get; set; }


        

        public int? iid_time_zone_user { get; set; }
        public string? vtime_zone_user { get; set; }
        public string? vnumber_document { get; set; }

        
    }

}
